namespace Kravets.Chatter.BLL.Contracts.Factories
{
    /// Represents factory that creates token.
    /// </summary>
    public interface ITokensFactory
    {
        /// <summary>
        /// Generates token.
        /// </summary>
        /// <param name="size">Size.</param>
        /// <returns>Token.</returns>
        string GenerateToken(int size = 32);
    }
}
