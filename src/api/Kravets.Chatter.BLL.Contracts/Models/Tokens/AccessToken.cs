namespace Kravets.Chatter.BLL.Contracts.Models.Tokens
{
    /// <summary>
    /// Represents access token model.
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="id">Token identifier.</param>
        /// <param name="token">Token.</param>
        public AccessToken(string id, string token) => (Id, Token) = (id, token);

        /// <summary>
        /// Initializes instance.
        /// </summary>
        public AccessToken() { }
    }
}
