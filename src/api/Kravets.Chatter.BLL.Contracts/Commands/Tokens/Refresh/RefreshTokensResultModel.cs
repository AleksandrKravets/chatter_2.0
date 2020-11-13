namespace Kravets.Chatter.BLL.Contracts.Commands.Tokens.Refresh
{
    /// <summary>
    /// Represents model that contains tokens.
    /// </summary>
    public class RefreshTokensResultModel
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
        public RefreshTokensResultModel(string accessToken, string refreshToken) =>
            (AccessToken, RefreshToken) = (accessToken, refreshToken);
    }
}
