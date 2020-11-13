using Kravets.Chatter.BLL.Contracts.Commands.Mutes.GetList;
using Kravets.Chatter.DAL.Contracts.Queries.Mutes;
using Kravets.Chatter.DAL.Contracts.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.Mutes
{
    /// <summary>
    /// Represents handler to get muted users for user by user identifier.
    /// </summary>
    public class GetMutedUsersByUserIdRequestHandler : IRequestHandler<GetMutedUsersByUserIdRequest, IEnumerable<MutedUserModel>>
    {
        private readonly IMutesRepository _mutesRepository;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="mutesRepository">Mutes repository provider.</param>
        public GetMutedUsersByUserIdRequestHandler(IMutesRepository mutesRepository)
        {
            _mutesRepository = mutesRepository;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of <see cref="MutedUserModel"></see>.</returns>
        public async Task<IEnumerable<MutedUserModel>> Handle(GetMutedUsersByUserIdRequest request, CancellationToken cancellationToken)
        {
            var mutedUsers = await _mutesRepository.GetAsync(
                new GetMutedUsersByUserIdQuery.Parameters(request.UserId), cancellationToken);

            var result = mutedUsers.Select(x => new MutedUserModel(x.MutedUserId));

            return result;
        }
    }
}
