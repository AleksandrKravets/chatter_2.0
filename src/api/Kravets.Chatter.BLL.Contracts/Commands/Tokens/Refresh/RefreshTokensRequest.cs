using Kravets.Chatter.BLL.Contracts.Models.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Kravets.Chatter.BLL.Contracts.Commands.Tokens.Refresh
{
    /// <summary>
    /// Represents request to refresh tokens.
    /// </summary>
    public class RefreshTokensRequest : IRequest<OneOf<Success<RefreshTokensResultModel>, BLError>>
    {
        /// <summary>
        /// Access token.
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Refresh token.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <param name="refreshToken">Refresh token.</param>
        public RefreshTokensRequest(string accessToken, string refreshToken) => 
            (AccessToken, RefreshToken) = (accessToken, refreshToken);
    }
}
