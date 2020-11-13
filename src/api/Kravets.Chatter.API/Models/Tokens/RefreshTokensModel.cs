using FluentValidation;

namespace Kravets.Chatter.API.Models.Tokens
{
    /// <summary>
    /// Represents model that contains tokens.
    /// </summary>
    public class RefreshTokensModel
    {
        /// <summary>
        /// Access token.
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Refresh token.
        /// </summary>
        public string RefreshToken { get; set; }

        public class Validator : AbstractValidator<RefreshTokensModel>
        {
            public Validator()
            {
                RuleFor(x => x.AccessToken).NotEmpty();
                RuleFor(x => x.RefreshToken).NotEmpty();
            }
        }
    }
}
