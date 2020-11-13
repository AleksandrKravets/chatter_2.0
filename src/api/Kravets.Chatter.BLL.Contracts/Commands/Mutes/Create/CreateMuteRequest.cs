using Kravets.Chatter.BLL.Contracts.Models.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Kravets.Chatter.BLL.Contracts.Commands.Mutes.Create
{
    /// <summary>
    /// Represents request to mute user by identifier.
    /// </summary>
    public class CreateMuteRequest : IRequest<OneOf<Success, BLError>>
    {
        /// <summary>
        /// Logged user identifier.
        /// </summary>
        public long UserId { get; private set; }
        /// <summary>
        /// User to mute identifier.
        /// </summary>
        public long UserToMuteId { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="userId">Logged user identifier.</param>
        /// <param name="userToMuteId">User to mute identifier.</param>
        public CreateMuteRequest(long userId, long userToMuteId)
        {
            UserId = userId;
            UserToMuteId = userToMuteId;
        }
    }
}
