using FluentValidation;

namespace Kravets.Chatter.API.Models.Messages
{
    /// <summary>
    /// Represents reply model.
    /// </summary>
    public class CreateReplyModel
    {
        /// <summary>
        /// Reply text.
        /// </summary>
        public string Text { get; set; }

        public class Validator : AbstractValidator<CreateReplyModel>
        {
            public Validator()
            {
                RuleFor(x => x.Text).NotEmpty();
            }
        }
    }
}
