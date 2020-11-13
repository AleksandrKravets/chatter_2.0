using Kravets.Chatter.BLL.Contracts.Models.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Kravets.Chatter.BLL.Contracts.Commands.Messages.Create
{
    /// <summary>
    /// Represents request to create message.
    /// </summary>
    public class CreateMessageRequest : IRequest<OneOf<Success<CreatedMessageModel>, BLError>> 
    {
        /// <summary>
        /// Message creator identifier.
        /// </summary>
        public long UserId { get; private set; }
        /// <summary>
        /// Message text.
        /// </summary>
        public string Text { get; private set; }
        /// <summary>
        /// Message to reply identifier.
        /// </summary>
        public long? MessageToReplyId { get; private set; }
        /// <summary>
        /// Is message reply.
        /// </summary>
        public bool IsReply { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="content">Message text.</param>
        /// <param name="isReply">Is message reply.</param>
        /// <param name="messageToReplyId">Message to reply identifier.</param>
        public CreateMessageRequest(long userId, string content, bool isReply, long? messageToReplyId) =>
            (UserId, Text, IsReply, MessageToReplyId) = (userId, content, isReply, messageToReplyId);
    }
}
