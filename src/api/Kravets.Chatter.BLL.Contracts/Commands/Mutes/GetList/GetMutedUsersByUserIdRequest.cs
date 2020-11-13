using MediatR;
using System.Collections.Generic;

namespace Kravets.Chatter.BLL.Contracts.Commands.Mutes.GetList
{
    /// <summary>
    /// Request to get user mutes.
    /// </summary>
    public class GetMutedUsersByUserIdRequest : IRequest<IEnumerable<MutedUserModel>>
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        public GetMutedUsersByUserIdRequest(long userId) => (UserId) = (userId);
    }
}
