using Kravets.Chatter.BLL.Contracts.Commands.Messages.Update;
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
    /// Represents handler to update message.
    /// </summary>
    public class UpdateMessageRequestHandler : IRequestHandler<UpdateMessageRequest, OneOf<Success, BLError>>
    {
        private readonly IMessagesRepository _messagesRepository;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="messagesRepository">Messages repository provider.</param>
        public UpdateMessageRequestHandler(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Unit or BLError.</returns>
        public async Task<OneOf<Success, BLError>> Handle(UpdateMessageRequest request, CancellationToken cancellationToken)
        {
            var message = await _messagesRepository.GetAsync(
                new GetMessageByIdQuery.Parameters(request.Id), cancellationToken);

            if (message.HasNoValue)
                return new BLError(ErrorMessages.ThereIsNoMessageWithSuchId);

            if (message.Value.UserId != request.UserId)
                return new BLError(ErrorMessages.MessageDoesntBelongToUser);

            await _messagesRepository.UpdateAsync(
                new UpdateMessageQuery.Parameters(request.Id, request.Text), cancellationToken);

            return new Success();
        }
    }
}
