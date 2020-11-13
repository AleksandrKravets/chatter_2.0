namespace Kravets.Chatter.BLL.Contracts.Hashers
{
    /// <summary>
    /// Represents password hasher.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes password.
        /// </summary>
        /// <param name="password">Password.</param>
        /// <param name="iterations">Number of iterations.</param>
        /// <returns>Hashed password.</returns>
        string Hash(string password, int iterations);
        /// <summary>
        /// Hashes password.
        /// </summary>
        /// <param name="password">Password.</param>
        /// <returns>Hashed password.</returns>
        string Hash(string password);
        /// <summary>
        /// Verifies password.
        /// </summary>
        /// <param name="password">Password to verify.</param>
        /// <param name="hashedPassword">Hashed password.</param>
        /// <returns>Boolean value.</returns>
        bool Verify(string password, string hashedPassword);
    }
}
