using FluentValidation;

namespace Kravets.Chatter.API.Models.Messages
{
    /// <summary>
    /// Represents new message model.
    /// </summary>
    public class CreateMessageModel
    {
        /// <summary>
        /// Message text.
        /// </summary>
        public string Text { get; set; }

        public class Validator : AbstractValidator<CreateMessageModel>
        {
            public Validator()
            {
                RuleFor(x => x.Text).NotEmpty();
            }
        }
    }
}
