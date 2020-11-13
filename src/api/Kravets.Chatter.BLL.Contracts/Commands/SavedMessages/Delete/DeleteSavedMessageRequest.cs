using Kravets.Chatter.BLL.Contracts.Models.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Kravets.Chatter.BLL.Contracts.Commands.SavedMessages.Delete
{
    /// <summary>
    /// Represents request to delete saved message.
    /// </summary>
    public class DeleteSavedMessageRequest : IRequest<OneOf<Success, BLError>>
    {
        /// <summary>
        /// Saved message identifier.
        /// </summary>
        public long Id { get; private set; }
        /// <summary>
        /// User identifier.
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="id">Saved message identifier.</param>
        /// <param name="userId">User identifier.</param>
        public DeleteSavedMessageRequest(long id, long userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
