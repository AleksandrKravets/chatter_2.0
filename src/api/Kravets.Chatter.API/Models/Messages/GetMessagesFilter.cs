using FluentValidation;

namespace Kravets.Chatter.API.Models.Messages
{
    /// <summary>
    /// Filter model to get messages.
    /// </summary>
    public class GetMessagesFilter
    {
        /// <summary>
        /// Last message identifier.
        /// </summary>
        public long? LastMessageId { get; set; }
        /// <summary>
        /// Page size.
        /// </summary>
        public int PageSize { get; set; } = 20;

        public class Validator : AbstractValidator<GetMessagesFilter>
        {
            public Validator()
            {
                RuleFor(x => x.PageSize).GreaterThan(0);
                // RuleFor(x => x.LastMessageId).GreaterThan(0);
            }
        }
    }
}
