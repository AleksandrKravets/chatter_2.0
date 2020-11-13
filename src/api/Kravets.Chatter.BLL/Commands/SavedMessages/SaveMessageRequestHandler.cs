using Kravets.Chatter.BLL.Contracts.Commands.SavedMessages.Save;
using Kravets.Chatter.BLL.Contracts.Models.Responses;
using Kravets.Chatter.Common.ResponseMessages;
using Kravets.Chatter.DAL.Contracts.Queries.Messages;
using Kravets.Chatter.DAL.Contracts.Queries.SavedMessages;
using Kravets.Chatter.DAL.Contracts.Repositories;
using MediatR;
using OneOf;
using OneOf.Types;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.SavedMessages
{
    /// <summary>
    /// Represents request to save message.
    /// </summary>
    public class SaveMessageRequestHandler : IRequestHandler<SaveMessageRequest, OneOf<Success, BLError>>
    {
        private readonly ISavedMessagesRepository _savedMessagesRepository;
        private readonly IMessagesRepository _messagesRepository;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="savedMessagesRepository">Saved messages repository provider.</param>
        /// <param name="messagesRepository">Messages repository provider.</param>
        public SaveMessageRequestHandler(
            ISavedMessagesRepository savedMessagesRepository, 
            IMessagesRepository messagesRepository)
        {
            _savedMessagesRepository = savedMessagesRepository;
            _messagesRepository = messagesRepository;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Success or BL error.</returns>
        public async Task<OneOf<Success, BLError>> Handle(SaveMessageRequest request, CancellationToken cancellationToken)
        {
            var message = await _messagesRepository.GetAsync(
                new GetMessageByIdQuery.Parameters(request.MessageId), cancellationToken);

            if (message.HasNoValue)
            {
                return new BLError(ErrorMessages.ThereIsNoMessageWithSuchId);
            }

            await _savedMessagesRepository.SaveAsync(
                new SaveMessageQuery.Parameters(request.UserId, request.MessageId), cancellationToken);

            return new Success();
        }
    }
}
