using Kravets.Chatter.BLL.Contracts.Models.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Kravets.Chatter.BLL.Contracts.Commands.SavedMessages.Save
{
    /// <summary>
    /// Represents request to save message.
    /// </summary>
    public class SaveMessageRequest : IRequest<OneOf<Success, BLError>>
    {
        /// <summary>
        /// Message identifier.
        /// </summary>
        public long MessageId { get; private set; }
        /// <summary>
        /// User identifier.
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="messageId">Message identifier.</param>
        /// <param name="userId">User identifier.</param>
        public SaveMessageRequest(long messageId, long userId)
        {
            MessageId = messageId;
            UserId = userId;
        }
    }
}
