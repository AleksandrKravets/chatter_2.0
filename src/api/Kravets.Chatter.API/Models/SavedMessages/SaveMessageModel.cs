using FluentValidation;

namespace Kravets.Chatter.API.Models.SavedMessages
{
    /// <summary>
    /// Represents message to save model.
    /// </summary>
    public class SaveMessageModel
    {
        /// <summary>
        /// Message to save identifier.
        /// </summary>
        public long MessageId { get; set; }

        public class Validator : AbstractValidator<SaveMessageModel>
        {
            public Validator()
            {
                RuleFor(x => x.MessageId).GreaterThan(0);
            }
        }
    }
}
