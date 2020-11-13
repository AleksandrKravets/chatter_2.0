using Kravets.Chatter.BLL.Contracts.Commands.OnlineUsers.GetList;
using Kravets.Chatter.BLL.Contracts.Services;
using Kravets.Chatter.DAL.Contracts.Queries.Mutes;
using Kravets.Chatter.DAL.Contracts.Repositories;
using MediatR;
using OneOf.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.OnlineUsers
{
    /// <summary>
    /// Represents handler to get online users.
    /// </summary>
    public class GetOnlineUsersRequestHandler : IRequestHandler<GetOnlineUsersRequest, Success<IEnumerable<OnlineUserModel>>>
    {
        private readonly IMutesRepository _mutesRepository;
        private readonly IOnlineUsersService _onlineUsersService;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="mutesRepository">Mutes repository provider.</param>
        /// <param name="onlineUsersService">Online users service provider.</param>
        public GetOnlineUsersRequestHandler(
            IMutesRepository mutesRepository,
            IOnlineUsersService onlineUsersService)
        {
            _mutesRepository = mutesRepository;
            _onlineUsersService = onlineUsersService;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Success<IEnumerable<OnlineUserModel>>> Handle(GetOnlineUsersRequest request, CancellationToken cancellationToken)
        {
            var mutes = await _mutesRepository.GetAsync(new GetMutedUsersByUserIdQuery.Parameters(request.UserId), cancellationToken);

            var onlineUsers = _onlineUsersService.Get();

            var result = onlineUsers.Select(x => new OnlineUserModel(x.Id, x.Nickname, mutes.Any(y => y.MutedUserId == x.Id), x.Id == request.UserId));

            return new Success<IEnumerable<OnlineUserModel>>(result);
        }
    }
}
