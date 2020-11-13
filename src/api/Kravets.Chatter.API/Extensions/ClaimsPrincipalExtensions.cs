using Kravets.Chatter.Common.ResponseMessages;
using System;
using System.Security.Claims;

namespace Kravets.Chatter.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetLongClaim(this ClaimsPrincipal principal, string key)
        {
            var claim = principal?.FindFirst(key);

            if (claim != null && long.TryParse(claim.Value, out var value))
                return value;

            throw new ApplicationException(string.Format(ErrorMessages.ClaimNotFound, key));
        }

        public static string GetStringClaim(this ClaimsPrincipal principal, string key)
        {
            var claim = principal?.FindFirst(key);

            if (claim != null)
                return claim.Value;

            throw new ApplicationException(string.Format(ErrorMessages.ClaimNotFound, key));
        }

        public static long? GetNullLongClaim(this ClaimsPrincipal principal, string key)
        {
            var claim = principal?.FindFirst(key);

            if (claim != null && long.TryParse(claim.Value, out var value))
                return value;

            return null;
        }
    }
}
