using Kravets.Chatter.BLL.Contracts.Factories;
using Kravets.Chatter.BLL.Contracts.Models.Tokens;
using Kravets.Chatter.BLL.Helpers;
using Kravets.Chatter.Common.Constants;
using Kravets.Chatter.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Kravets.Chatter.BLL.Factories
{
    /// <inheritdoc />
    public class JwtTokensFactory : IJwtTokensFactory
    {
        private readonly JwtSettings _settings;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="jwtSettings">JWT settings.</param>
        public JwtTokensFactory(IOptions<JwtSettings> jwtSettings)
        {
            _settings = jwtSettings?.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
        }

        private IEnumerable<Claim> GetClaims(long userId, string nickname)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nickname),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(CustomClaims.UserId, userId.ToString()),
                new Claim(CustomClaims.Nickname, nickname)
            };

            return claims;
        }

        /// <inheritdoc />
        public AccessToken GenerateToken(long userId, string nickname)
        {
            if (userId < 1 || string.IsNullOrEmpty(nickname))
                return null;

            var claims = GetClaims(userId, nickname);

            var key = JwtHelper.GetPrivateKey(_settings.PrivateKey);
            var algorithm = SecurityAlgorithms.RsaSha256;
            var signingCredentials = new SigningCredentials(key, algorithm);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                NotBefore = DateTime.UtcNow,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_settings.AccessTokenLifetime),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            string accessToken = tokenHandler.WriteToken(token);

            return new AccessToken(token.Id, accessToken);
        }
    }
}
