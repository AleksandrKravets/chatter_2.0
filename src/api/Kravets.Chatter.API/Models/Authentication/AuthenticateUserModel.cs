using FluentValidation;

namespace Kravets.Chatter.API.Models.Authentication
{
    /// <summary>
    /// Represents model that contains user credentials.
    /// </summary>
    public class AuthenticateUserModel
    {
        /// <summary>
        /// Nickname.
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }

        public class Validator : AbstractValidator<AuthenticateUserModel>
        {
            public Validator()
            {
                RuleFor(x => x.Nickname).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
    }
}
