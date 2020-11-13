using Kravets.Chatter.BLL.Contracts.Models.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Kravets.Chatter.BLL.Contracts.Commands.Messages.Delete
{
    /// <summary>
    /// Represents request to delete message.
    /// </summary>
    public class DeleteMessageRequest : IRequest<OneOf<Success, BLError>>
    {
        /// <summary>
        /// Current user identifier.
        /// </summary>
        public long UserId { get; private set; }
        /// <summary>
        /// Message identifier.
        /// </summary>
        public long MessageId { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="messageId">Message identifier.</param>
        public DeleteMessageRequest(long userId, long messageId) => 
            (UserId, MessageId) = (userId, messageId);
    }
}