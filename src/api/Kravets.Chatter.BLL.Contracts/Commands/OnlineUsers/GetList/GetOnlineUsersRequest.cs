using MediatR;
using OneOf.Types;
using System.Collections.Generic;

namespace Kravets.Chatter.BLL.Contracts.Commands.OnlineUsers.GetList
{
    /// <summary>
    /// Represents request to get online users.
    /// </summary>
    public class GetOnlineUsersRequest : IRequest<Success<IEnumerable<OnlineUserModel>>>
    {
        /// <summary>
        /// Current logged user identifier.
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="userId">Current logged user identifier.</param>
        public GetOnlineUsersRequest(long userId) => (UserId) = (userId);
    }
}
