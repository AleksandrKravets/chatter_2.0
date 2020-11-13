using FluentValidation;

namespace Kravets.Chatter.API.Models.SavedMessages
{
    /// <summary>
    /// Filter model to get messages.
    /// </summary>
    public class GetSavedMessagesFilter
    {
        /// <summary>
        /// Page index.
        /// </summary>
        public int PageIndex { get; set; } = 0;
        /// <summary>
        /// Page size.
        /// </summary>
        public int PageSize { get; set; } = 20;

        public class Validator : AbstractValidator<GetSavedMessagesFilter>
        {
            public Validator()
            {
                RuleFor(x => x.PageSize).GreaterThan(0);
                RuleFor(x => x.PageIndex).GreaterThan(-1);
            }
        }
    }
}
