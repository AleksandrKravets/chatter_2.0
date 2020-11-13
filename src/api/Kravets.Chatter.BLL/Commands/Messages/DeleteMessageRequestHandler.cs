using Kravets.Chatter.BLL.Contracts.Commands.Messages.Delete;
using Kravets.Chatter.BLL.Contracts.Models.Responses;
using Kravets.Chatter.Common.ResponseMessages;
using Kravets.Chatter.DAL.Contracts.Queries.Messages;
using Kravets.Chatter.DAL.Contracts.Repositories;
using MediatR;
using OneOf;
using OneOf.Types;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.Messages
{
    /// <summary>
    /// Represents request to delete message by identifier.
    /// </summary>
    public class DeleteMessageRequestHandler : IRequestHandler<DeleteMessageRequest, OneOf<Success, BLError>>
    {
        private readonly IMessagesRepository _messagesRepository;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="messagesRepository">Messages repository provider.</param>
        public DeleteMessageRequestHandler(IMessagesRepository messagesRepository) =>
            (_messagesRepository) = (messagesRepository);

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<OneOf<Success, BLError>> Handle(DeleteMessageRequest request, CancellationToken cancellationToken)
        {
            var message = await _messagesRepository.GetAsync(
                new GetMessageByIdQuery.Parameters(request.MessageId), cancellationToken);

            if (message.HasNoValue)
            {
                return new BLError(ErrorMessages.ThereIsNoMessageWithSuchId);
            }

            if(message.Value.UserId != request.UserId)
            {
                return new BLError(ErrorMessages.MessageDoesntBelongToUser);
            }

            await _messagesRepository.DeleteAsync(
                new DeleteMessageQuery.Parameters(request.MessageId),
                cancellationToken);

            return new Success();
        }
    }
}
