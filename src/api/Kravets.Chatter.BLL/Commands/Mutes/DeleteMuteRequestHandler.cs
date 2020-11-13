using Kravets.Chatter.BLL.Contracts.Commands.Mutes.Delete;
using Kravets.Chatter.DAL.Contracts.Queries.Mutes;
using Kravets.Chatter.DAL.Contracts.Repositories;
using MediatR;
using OneOf.Types;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.Mutes
{
    /// <summary>
    /// Represents handler to unmute user by identifier.
    /// </summary>
    public class DeleteMuteRequestHandler : IRequestHandler<DeleteMuteRequest, Success>
    {
        private readonly IMutesRepository _mutesRepository;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="mutesRepository">Mutes repository provider.</param>
        public DeleteMuteRequestHandler(IMutesRepository mutesRepository)
        {
            _mutesRepository = mutesRepository;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Success or BL error.</returns>
        public async Task<Success> Handle(DeleteMuteRequest request, CancellationToken cancellationToken)
        {
            await _mutesRepository.DeleteAsync(
                new DeleteMuteQuery.Parameters(request.UserId, request.UserToUnmuteId), cancellationToken);

            return new Success();
        }
    }
}
