using Kravets.Chatter.BLL.Contracts.Commands.Authentication.Authenticate;
using Kravets.Chatter.BLL.Contracts.Factories;
using Kravets.Chatter.BLL.Contracts.Hashers;
using Kravets.Chatter.BLL.Contracts.Models.Responses;
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
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.Authentication
{
    /// <summary>
    /// Represents authentication request handler.
    /// </summary>
    public class AuthenticationRequestHandler : IRequestHandler<AuthenticationRequest, OneOf<Success<AuthenticationResultModel>, BLError>>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokensFactory _jwtTokensFactory;
        private readonly ITokensFactory _tokensFactory;
        private readonly ITokensRepository _tokensRepository;
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="usersRepository">Users repository provider.</param>
        /// <param name="passwordHasher">Password hasher provider.</param>
        /// <param name="jwtTokensFactory">JWT tokens factory provider.</param>
        /// <param name="tokensFactory">Tokens factory provider.</param>
        /// <param name="tokensRepository">Tokens repository provider.</param>
        /// <param name="jwtSettings">JWT settings.</param>
        public AuthenticationRequestHandler(
            IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            IJwtTokensFactory jwtTokensFactory,
            ITokensFactory tokensFactory,
            ITokensRepository tokensRepository,
            IOptions<JwtSettings> jwtSettings)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtTokensFactory = jwtTokensFactory;
            _tokensFactory = tokensFactory;
            _tokensRepository = tokensRepository;
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Success or BLError.</returns>
        public async Task<OneOf<Success<AuthenticationResultModel>, BLError>> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetAsync(new GetUserByNicknameQuery.Parameters(request.Nickname), cancellationToken);

            if (user.HasNoValue)
                return new BLError(ErrorMessages.UserWithSuchNicknameDoesNotExist);

            //bool passwordCorrect = _passwordHasher.Verify(request.Password, user.Value.HashedPassword);

            //if (!passwordCorrect)
            //    return new BLError(ErrorMessages.PasswordIsIncorrect);

            var accessToken = _jwtTokensFactory.GenerateToken(user.Value.Id, user.Value.Nickname);
            var refreshToken = _tokensFactory.GenerateToken();

            await _tokensRepository.UpsertAsync(new UpsertTokenQuery.Parameters(
                jwtId: accessToken.Id,
                creationTime: DateTime.UtcNow,
                expiryTime: DateTime.UtcNow.AddSeconds(_jwtSettings.RefreshTokenLifetime),
                token: refreshToken,
                userId: user.Value.Id), cancellationToken);

            var result = new AuthenticationResultModel(accessToken.Token, refreshToken);

            return new Success<AuthenticationResultModel>(result);
        }
    }
}
