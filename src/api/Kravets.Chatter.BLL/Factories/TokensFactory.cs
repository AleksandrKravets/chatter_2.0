using Kravets.Chatter.BLL.Contracts.Factories;
using System;
using System.Security.Cryptography;

namespace Kravets.Chatter.BLL.Factories
{
    /// <inheritdoc />
    public class TokensFactory : ITokensFactory
    {
        /// <inheritdoc />
        public string GenerateToken(int size)
        {
            var randomNumber = new byte[size];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            var token = Convert.ToBase64String(randomNumber);

            return token;
        }
    }
}
