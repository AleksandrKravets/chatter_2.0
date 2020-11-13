using Kravets.Chatter.BLL.Contracts.Models.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Kravets.Chatter.BLL.Contracts.Commands.Authentication.Authenticate
{
    /// <summary>
    /// Represents request to authenticate user.
    /// </summary>
    public class AuthenticationRequest : IRequest<OneOf<Success<AuthenticationResultModel>, BLError>>
    {
        /// <summary>
        /// Nickname.
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="nickname">User nickname.</param>
        /// <param name="password">User password.</param>
        public AuthenticationRequest(string nickname, string password) => 
            (Nickname, Password) = (nickname, password);
    }
}
