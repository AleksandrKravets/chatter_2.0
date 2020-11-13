using FluentValidation;

namespace Kravets.Chatter.API.Models.Messages
{
    /// <summary>
    /// Represents new message model.
    /// </summary>
    public class UpdateMessageModel
    {
        /// <summary>
        /// Message text.
        /// </summary>
        public string Text { get; set; }

        public class Validator : AbstractValidator<UpdateMessageModel>
        {
            public Validator()
            {
                RuleFor(x => x.Text).NotEmpty();
            }
        }
    }
}
