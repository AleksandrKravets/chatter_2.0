using CSharpFunctionalExtensions;
using Kravets.Chatter.BLL.Contracts.Commands.Tokens.Refresh;
using Kravets.Chatter.BLL.Contracts.Factories;
using Kravets.Chatter.BLL.Contracts.Models.Responses;
using Kravets.Chatter.BLL.Helpers;
using Kravets.Chatter.Common.Constants;
using Kravets.Chatter.Common.ResponseMessages;
using Kravets.Chatter.Common.Settings;
using Kravets.Chatter.DAL.Contracts.Queries.Tokens;
using Kravets.Chatter.DAL.Contracts.Queries.Users;
using Kravets.Chatter.DAL.Contracts.Repositories;
using MediatR;
using Microsoft.Extensions.Options;
using OneOf;
using OneOf.Types;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.Tokens
{
    /// <summary>
    /// Represents handler to refresh tokens.
    /// </summary>
    public class RefreshTokensRequestHandler : IRequestHandler<RefreshTokensRequest, OneOf<Success<RefreshTokensResultModel>, BLError>>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITokensRepository _tokensRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly IJwtTokensFactory _jwtTokensFactory;
        private readonly ITokensFactory _tokensFactory;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="usersRepository">Users repository provider.</param>
        /// <param name="tokensRepository">Tokens repository provider.</param>
        /// <param name="jwtSettings">JWT settings.</param>
        /// <param name="jwtTokensFactory">JWT tokens factory provider.</param>
        /// <param name="tokensFactory">Tokens factory provider.</param>
        public RefreshTokensRequestHandler(
            IUsersRepository usersRepository,
            ITokensRepository tokensRepository,
            IOptions<JwtSettings> jwtSettings,
            IJwtTokensFactory jwtTokensFactory,
            ITokensFactory tokensFactory)
        {
            _usersRepository = usersRepository;
            _tokensRepository = tokensRepository;
            _jwtSettings = jwtSettings.Value;
            _jwtTokensFactory = jwtTokensFactory;
            _tokensFactory = tokensFactory;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The canellation token.</param>
        /// <returns>Tokens or BLError</returns>
        public async Task<OneOf<Success<RefreshTokensResultModel>, BLError>> Handle(RefreshTokensRequest request, CancellationToken cancellationToken)
        {
            var principal = JwtHelper.GetPrincipalFromExpiredToken(request.AccessToken, _jwtSettings);

            if (principal == null)
                return new BLError(ErrorMessages.AccessTokenIsNotValid);

            var token = await _tokensRepository.GetAsync(new GetTokenQuery.Parameters(request.RefreshToken), cancellationToken);

            bool tokenValid = IsTokenValid(principal, token);

            if (!tokenValid)
                return new BLError(ErrorMessages.RefreshTokenIsNotValid);

            var userId = Convert.ToInt64(principal.Claims.SingleOrDefault(x => x.Type == CustomClaims.UserId)?.Value);

            var user = await _usersRepository.GetAsync(new GetUserByIdQuery.Parameters(userId), cancellationToken);

            if (user.HasNoValue)
                return new BLError(ErrorMessages.NoUserWithSuchIdExists);

            var accessToken = _jwtTokensFactory.GenerateToken(user.Value.Id, user.Value.Nickname);

            var refreshToken = _tokensFactory.GenerateToken();

            await _tokensRepository.UpsertAsync(
                new UpsertTokenQuery.Parameters(
                    jwtId: accessToken.Id,
                    creationTime: DateTime.UtcNow,
                    expiryTime: DateTime.UtcNow.AddSeconds(_jwtSettings.RefreshTokenLifetime),
                    token: refreshToken,
                    userId: user.Value.Id), cancellationToken);

            var result = new RefreshTokensResultModel(accessToken.Token, refreshToken);

            return new Success<RefreshTokensResultModel>(result);
        }

        private bool IsTokenValid(ClaimsPrincipal principal, Maybe<GetTokenQuery.Result> token)
        {
            if (principal == null || token.HasNoValue)
            {
                return false;
            }

            var expiryDateUnix =
                long.Parse(principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            var jti = principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            return expiryDateTimeUtc > DateTime.UtcNow || token == null ||
                DateTime.UtcNow > token.Value.ExpiryTime || token.Value.JwtId != jti; // is used check here
        }
    }
}
