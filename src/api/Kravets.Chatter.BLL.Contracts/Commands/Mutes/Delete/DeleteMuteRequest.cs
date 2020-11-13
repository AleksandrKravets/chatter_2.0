using MediatR;
using OneOf.Types;

namespace Kravets.Chatter.BLL.Contracts.Commands.Mutes.Delete
{
    /// <summary>
    /// Represents request to unmute user by identifier.
    /// </summary>
    public class DeleteMuteRequest : IRequest<Success>
    {
        /// <summary>
        /// Logged user identifier.
        /// </summary>
        public long UserId { get; private set; }
        /// <summary>
        /// User to unmute identifier.
        /// </summary>
        public long UserToUnmuteId { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="userId">Logged user identifier.</param>
        /// <param name="userToUnmuteId">User to unmute identifier.</param>
        public DeleteMuteRequest(long userId, long userToUnmuteId)
        {
            UserId = userId;
            UserToUnmuteId = userToUnmuteId;
        }
    }
}
