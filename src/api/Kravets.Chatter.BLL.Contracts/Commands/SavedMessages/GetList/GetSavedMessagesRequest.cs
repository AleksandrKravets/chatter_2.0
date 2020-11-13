using Kravets.Chatter.BLL.Contracts.Models.Collections;
using MediatR;

namespace Kravets.Chatter.BLL.Contracts.Commands.SavedMessages.GetList
{
    /// <summary>
    /// Represents request to get messages.
    /// </summary>
    public class GetSavedMessagesRequest : IRequest<PagedListModel<SavedMessageModel>>
    {
        /// <summary>
        /// Page index.
        /// </summary>
        public int PageIndex { get; private set; }
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
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="userId">User identifier.</param>
        public GetSavedMessagesRequest(int pageIndex, int pageSize, long userId) =>
            (PageIndex, PageSize, UserId) = (pageIndex, pageSize, userId);
    }
}
