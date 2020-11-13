namespace Kravets.Chatter.BLL.Contracts.Commands.Authentication.Authenticate
{
    /// <summary>
    /// Represents authentication result.
    /// </summary>
    public class AuthenticationResultModel
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
        /// <param name="success">True if success.</param>
        public AuthenticationResultModel(string accessToken, string refreshToken) =>
            (AccessToken, RefreshToken) = (accessToken, refreshToken);
    }
}
