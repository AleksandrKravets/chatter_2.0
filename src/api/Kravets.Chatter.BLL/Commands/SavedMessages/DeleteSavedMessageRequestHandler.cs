using Kravets.Chatter.BLL.Contracts.Commands.SavedMessages.Delete;
using Kravets.Chatter.BLL.Contracts.Models.Responses;
using Kravets.Chatter.Common.ResponseMessages;
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
    /// Represents request to delete saved message.
    /// </summary>
    public class DeleteSavedMessageRequestHandler : IRequestHandler<DeleteSavedMessageRequest, OneOf<Success, BLError>>
    {
        private readonly ISavedMessagesRepository _savedMessagesRepository;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="savedMessagesRepository">Saved messages repository provider.</param>
        public DeleteSavedMessageRequestHandler(ISavedMessagesRepository savedMessagesRepository)
        {
            _savedMessagesRepository = savedMessagesRepository;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Success or BL error.</returns>
        public async Task<OneOf<Success, BLError>> Handle(DeleteSavedMessageRequest request, CancellationToken cancellationToken)
        {
            var savedMessage = await _savedMessagesRepository.GetAsync(
                new GetSavedMessageQuery.Parameters(request.Id), cancellationToken);

            if(savedMessage.HasNoValue)
            {
                return new BLError(ErrorMessages.ThereIsNoSavedMessageWithSuchId);
            }

            if(savedMessage.Value.SavedMessageOwnerId != request.UserId)
            {
                return new BLError(ErrorMessages.SavedMessageDoesNotBelongToUser);
            }

            await _savedMessagesRepository.DeleteAsync(
                new DeleteSavedMessageQuery.Parameters(request.Id), cancellationToken);

            return new Success();
        }
    }
}
