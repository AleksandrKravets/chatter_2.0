using Kravets.Chatter.DAL.Contracts.Queries.Mutes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.DAL.Contracts.Repositories
{
    /// <summary>
    /// Represents repository to manage mutes.
    /// </summary>
    public interface IMutesRepository
    {
        /// <summary>
        /// Returns muted users for user by user identifier.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of <see cref="GetMutedUsersByUserIdQuery.Result"></see>.</returns>
        Task<IEnumerable<GetMutedUsersByUserIdQuery.Result>> GetAsync(
            GetMutedUsersByUserIdQuery.Parameters parameters, 
            CancellationToken cancellationToken);

        /// <summary>
        /// Creates mute.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task CreateAsync(CreateMuteQuery.Parameters parameters, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes mute.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task DeleteAsync(DeleteMuteQuery.Parameters parameters, CancellationToken cancellationToken);
    }
}
