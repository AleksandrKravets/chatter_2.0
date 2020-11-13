using FluentValidation;

namespace Kravets.Chatter.API.Models.Messages
{
    /// <summary>
    /// Represents filter to get lost messages.
    /// </summary>
    public class GetLostMessagesFilter
    {
        /// <summary>
        /// Last message identifier.
        /// </summary>
        public long LastMessageId { get; set; }

        public class Validator : AbstractValidator<GetLostMessagesFilter>
        {
            public Validator()
            {
                RuleFor(x => x.LastMessageId).GreaterThan(0);
            }
        }
    }
}
