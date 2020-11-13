using Kravets.Chatter.BLL.Contracts.Commands.Messages.GetList;
using Kravets.Chatter.BLL.Contracts.Models.Collections;
using Kravets.Chatter.DAL.Contracts.Queries.Messages;
using Kravets.Chatter.DAL.Contracts.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.Messages
{
    /// <summary>
    /// Represents request to get messages.
    /// </summary>
    public class GetMessagesRequestHandler : IRequestHandler<GetMessagesRequest, PagedListModel<MessageModel>>
    {
        private readonly IMessagesRepository _messagesRepository;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="messagesRepository">Messages repository provider.</param>
        public GetMessagesRequestHandler(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of <see cref="MessageModel"></see>.</returns>
        public async Task<PagedListModel<MessageModel>> Handle(GetMessagesRequest request, CancellationToken cancellationToken)
        {
            var response = await _messagesRepository.GetAsync(
                new GetMessagesQuery.Parameters(request.LastMessageId, request.PageSize), 
                cancellationToken);

            var messages = response.Messages.Select(x => new MessageModel(x, request.UserId));

            // TODO: change model (There is no need to use PageIndex here) 
            var result = new PagedListModel<MessageModel>(
                messages, 0, request.PageSize, response.NextPageExists);

            return result;
        }
    }
}
