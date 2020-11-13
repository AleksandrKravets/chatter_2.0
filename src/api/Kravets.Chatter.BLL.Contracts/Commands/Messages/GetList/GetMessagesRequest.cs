using Kravets.Chatter.BLL.Contracts.Models.Collections;
using MediatR;

namespace Kravets.Chatter.BLL.Contracts.Commands.Messages.GetList
{
    /// <summary>
    /// Represents request to get messages.
    /// </summary>
    public class GetMessagesRequest : IRequest<PagedListModel<MessageModel>>
    {
        /// <summary>
        /// Last message identifier.
        /// </summary>
        public long? LastMessageId { get; set; }
        /// <summary>
        /// Page size.
        /// </summary>
        public int PageSize { get; private set; }
        /// <summary>
        /// Current user identifier.
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="lastMessageId">Last message identifier.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="userId">User identifier.</param>
        public GetMessagesRequest(long? lastMessageId, int pageSize, long userId) => 
            (LastMessageId, PageSize, UserId) = (lastMessageId, pageSize, userId);
    }
}
