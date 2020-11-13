using Kravets.Chatter.Common.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Kravets.Chatter.BLL.Helpers
{
    public static class JwtHelper
    {
        public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token, JwtSettings jwtSettings)
        {
            var validationParameters = 
                GetTokenValidationParameters(GetPublicKey(jwtSettings.PublicKey), jwtSettings.Issuer, jwtSettings.Audience);

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !IsJwtWithValidSecurityAlgorithm(jwtSecurityToken))
                    return null;

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public static SecurityKey GetPublicKey(string publicKey)
        {
            var key = RSA.Create();
            key.ImportRSAPublicKey(source: Convert.FromBase64String(publicKey), bytesRead: out var _);
            return new RsaSecurityKey(key);
        }

        public static SecurityKey GetPrivateKey(string privateKey)
        {
            var key = RSA.Create();
            key.ImportRSAPrivateKey(source: Convert.FromBase64String(privateKey), bytesRead: out var _);
            return new RsaSecurityKey(key);
        }

        public static TokenValidationParameters GetTokenValidationParameters(SecurityKey publicKey, string issuer, string audience)
        {
            return new TokenValidationParameters
            {
                RequireAudience = true,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = publicKey,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireSignedTokens = true,
            };
        }

        private static bool IsJwtWithValidSecurityAlgorithm(JwtSecurityToken jwtSecurityToken)
        {
            return jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
