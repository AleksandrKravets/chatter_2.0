using Kravets.Chatter.BLL.Contracts.Hashers;
using Kravets.Chatter.Common.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;

namespace Kravets.Chatter.BLL.Hashers
{
    /// <inheritdoc />
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasherSettings _settings;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="settings">Password hasher settings.</param>
        public PasswordHasher(IOptions<PasswordHasherSettings> settings)
        {
            _settings = settings.Value;
        }

        /// <inheritdoc />
        public string Hash(string password, int iterations)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt;
                rng.GetBytes(salt = new byte[_settings.SaltSize]);
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
                {
                    var hash = pbkdf2.GetBytes(_settings.HashSize);
                    var hashBytes = new byte[_settings.SaltSize + _settings.HashSize];
                    Array.Copy(salt, 0, hashBytes, 0, _settings.SaltSize);
                    Array.Copy(hash, 0, hashBytes, _settings.SaltSize, _settings.HashSize);
                    var base64Hash = Convert.ToBase64String(hashBytes);
                    return $"$HASH|V1${iterations}${base64Hash}";
                }
            }
        }

        /// <inheritdoc />
        public string Hash(string password)
        {
            return Hash(password, _settings.Iterations);
        }

        /// <inheritdoc />
        public bool Verify(string password, string hashedPassword)
        {
            if (!IsHashSupported(hashedPassword))
            {
                return false;
            }

            var splittedHashString = hashedPassword.Replace("$HASH|V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            var hashBytes = Convert.FromBase64String(base64Hash);

            var salt = new byte[_settings.SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, _settings.SaltSize);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(_settings.HashSize);

                for (var i = 0; i < _settings.HashSize; i++)
                    if (hashBytes[i + _settings.SaltSize] != hash[i])
                        return false;

                return true;
            }
        }

        private bool IsHashSupported(string hashString)
        {
            return hashString.Contains("HASH|V1$");
        }
    }
}
