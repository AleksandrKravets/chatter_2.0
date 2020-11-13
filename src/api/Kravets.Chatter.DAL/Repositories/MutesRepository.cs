using Kravets.Chatter.DAL.Contracts.Queries.Mutes;
using Kravets.Chatter.DAL.Contracts.Repositories;
using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.StoredProcedures.Mutes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.DAL.Repositories
{
    /// <inheritdoc />
    public class MutesRepository : IMutesRepository
    {
        private readonly StoredProcedureExecutor _procedureExecutor;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="procedureExecutor">Stored procedure executor.</param>
        public MutesRepository(StoredProcedureExecutor procedureExecutor)
        {
            _procedureExecutor = procedureExecutor;
        }

        /// <inheritdoc />
        public Task CreateAsync(CreateMuteQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return _procedureExecutor.ExecuteAsync(
                new spCreateMute
                {
                    UserId = parameters.UserId,
                    UserToMuteId = parameters.UserToMuteId
                }, cancellationToken);
        }

        /// <inheritdoc />
        public Task DeleteAsync(DeleteMuteQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return _procedureExecutor.ExecuteAsync(
                new spDeleteMute
                {
                    UserId = parameters.UserId,
                    UserToUnmuteId = parameters.UserToUnmuteId
                }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IEnumerable<GetMutedUsersByUserIdQuery.Result>> GetAsync(GetMutedUsersByUserIdQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return _procedureExecutor.ExecuteWithListResponseAsync<GetMutedUsersByUserIdQuery.Result>(
                new spGetMutes
                {
                    UserId = parameters.UserId
                }, cancellationToken);
        }
    }
}