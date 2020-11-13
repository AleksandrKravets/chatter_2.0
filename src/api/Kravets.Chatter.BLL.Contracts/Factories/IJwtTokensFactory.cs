using Kravets.Chatter.BLL.Contracts.Models.Tokens;

namespace Kravets.Chatter.BLL.Contracts.Factories
{
    /// <summary>
    /// Represents factory that creates JWT.
    /// </summary>
    public interface IJwtTokensFactory
    {
        /// <summary>
        /// Generates token based on user data.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="nickname">User nickname.</param>
        /// <returns>The instance of <see cref="AccessToken"></see>.</returns>
        AccessToken GenerateToken(long userId, string nickname);
    }
}
