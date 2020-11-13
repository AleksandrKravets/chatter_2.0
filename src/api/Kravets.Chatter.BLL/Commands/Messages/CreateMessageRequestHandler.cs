using Kravets.Chatter.BLL.Contracts.Commands.Messages.Create;
using Kravets.Chatter.BLL.Contracts.Models.Responses;
using Kravets.Chatter.Common.ResponseMessages;
using Kravets.Chatter.DAL.Contracts.Queries.Messages;
using Kravets.Chatter.DAL.Contracts.Queries.Users;
using Kravets.Chatter.DAL.Contracts.Repositories;
using MediatR;
using OneOf;
using OneOf.Types;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.Messages
{
    /// <summary>
    /// Represents message creation request handler.
    /// </summary>
    public class CreateMessageRequestHandler : IRequestHandler<CreateMessageRequest, OneOf<Success<CreatedMessageModel>, BLError>>
    {
        private readonly IMessagesRepository _messagesRepository;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="messagesRepository">Messages repository provider.</param>
        public CreateMessageRequestHandler(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Created message model.</returns>
        public async Task<OneOf<Success<CreatedMessageModel>, BLError>> Handle(CreateMessageRequest request, CancellationToken cancellationToken)
        {
            if (request.IsReply)
            {
                if (!request.MessageToReplyId.HasValue)
                    return new BLError(ErrorMessages.MessageToReplyIdIsRequired);

                var messageToReply = _messagesRepository.GetAsync(new GetMessageByIdQuery.Parameters(request.MessageToReplyId.Value), cancellationToken);

                if (messageToReply == null)
                    return new BLError(ErrorMessages.CantCreateReply);
            }

            var newMessageId = await _messagesRepository.CreateAsync(
                new CreateMessageQuery.Parameters(
                    request.UserId,
                    request.Text,
                    DateTime.Now, 
                    request.IsReply, 
                    request.MessageToReplyId),
                cancellationToken);

            var createdMessage = await _messagesRepository.GetAsync(
                new GetMessageByIdQuery.Parameters(newMessageId.CreatedMessageId), cancellationToken);

            if (createdMessage.HasNoValue)
            {
                return new BLError(ErrorMessages.SaveMessageError);
            }

            var result = new CreatedMessageModel(createdMessage.Value, request.UserId);

            return new Success<CreatedMessageModel>(result);
        }
    }
}
