using Kravets.Chatter.BLL.Contracts.Commands.OnlineUsers.GetCount;
using Kravets.Chatter.BLL.Contracts.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.OnlineUsers
{
    /// <summary>
    /// Represents handler to get count of online users.
    /// </summary>
    public class GetOnlineUsersCountRequestHandler : IRequestHandler<GetOnlineUsersCountRequest, int>
    {
        private readonly IOnlineUsersService _onlineUsersService;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="onlineUsersService">Online users service provider.</param>
        public GetOnlineUsersCountRequestHandler(IOnlineUsersService onlineUsersService)
        {
            _onlineUsersService = onlineUsersService;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Online users count.</returns>
        public Task<int> Handle(GetOnlineUsersCountRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_onlineUsersService.Count);
        }
    }
}
