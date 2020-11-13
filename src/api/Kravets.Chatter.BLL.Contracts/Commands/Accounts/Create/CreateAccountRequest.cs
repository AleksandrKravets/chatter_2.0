using Kravets.Chatter.BLL.Contracts.Models.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Kravets.Chatter.BLL.Contracts.Commands.Accounts.Create
{
    /// <summary>
    /// Represents request to create new user account.
    /// </summary>
    public class CreateAccountRequest : IRequest<OneOf<Success, BLError>>
    {
        /// <summary>
        /// User nickname.
        /// </summary>
        public string Nickname { get; private set; }
        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="nickname">User nickname.</param>
        /// <param name="password">User password.</param>
        public CreateAccountRequest(string nickname, string password) =>
            (Nickname, Password) = (nickname, password);
    }
}
