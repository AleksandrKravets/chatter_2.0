using FluentValidation;

namespace Kravets.Chatter.API.Models.Accounts
{
    /// <summary>
    /// Represents model for account registration.
    /// </summary>
    public class RegisterAccountModel
    {
        /// <summary>
        /// User nickname.
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; set; }

        public class Validator : AbstractValidator<RegisterAccountModel>
        {
            public Validator()
            {
                RuleFor(x => x.Nickname).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
    }
}
