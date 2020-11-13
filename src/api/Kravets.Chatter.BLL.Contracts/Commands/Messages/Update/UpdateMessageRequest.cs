using Kravets.Chatter.BLL.Contracts.Models.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Kravets.Chatter.BLL.Contracts.Commands.Messages.Update
{
    /// <summary>
    /// Represents request to update message.
    /// </summary>
    public class UpdateMessageRequest : IRequest<OneOf<Success, BLError>>
    {
        /// <summary>
        /// Message identifier.
        /// </summary>
        public long Id { get; private set; }
        /// <summary>
        /// New message text.
        /// </summary>
        public string Text { get; private set; }
        /// <summary>
        /// Current logged user identifier.
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="id">Message identifier.</param>
        /// <param name="text">Message text.</param>
        /// <param name="userId">User identifier.</param>
        public UpdateMessageRequest(long id, string text, long userId) => 
            (Id, Text, UserId) = (id, text, userId);
    }
}
