using Kravets.Chatter.BLL.Contracts.Commands.Messages.GetFromId;
using Kravets.Chatter.DAL.Contracts.Queries.Messages;
using Kravets.Chatter.DAL.Contracts.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.Messages
{
    /// <summary>
    /// It is a handler for receiving messages with Id greater than the Id from parameters.
    /// </summary>
    public class GetMessagesFromIdRequestHandler : IRequestHandler<GetMessagesFromIdRequest, IEnumerable<MessageModel>>
    {
        private readonly IMessagesRepository _messagesRepository;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="messagesRepository">Messages repository provider.</param>
        public GetMessagesFromIdRequestHandler(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of <see cref="MessageModel"></see>.</returns>
        public async Task<IEnumerable<MessageModel>> Handle(GetMessagesFromIdRequest request, CancellationToken cancellationToken)
        {
            var response = await _messagesRepository.GetFromIdAsync(
                new GetMessagesFromIdQuery.Parameters(request.Id),
                cancellationToken);

            var messages = response.Select(x => new MessageModel(x));

            return messages;
        }
    }
}
