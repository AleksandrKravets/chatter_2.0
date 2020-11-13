using Kravets.Chatter.DAL.Infrastructure.Commands;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.DAL.Infrastructure
{
    public class StoredProcedureExecutor
    {
        private readonly string _connectionString;

        public StoredProcedureExecutor(IOptions<DatabaseSettings> settings)
        {
            _connectionString = settings.Value.ConnectionString;
        }

        public Task<IEnumerable<TResult>> ExecuteWithListResponseAsync<TResult>(
            StoredProcedure storedProcedure, CancellationToken cancellationToken) where TResult : class
        {
            var command = new ExecuteWithListResponseCommand<TResult>(_connectionString);
            return command.ExecuteAsync(storedProcedure, cancellationToken);
        }

        public Task<TResult> ExecuteWithObjectResponseAsync<TResult>(
            StoredProcedure storedProcedure, CancellationToken cancellationToken) where TResult : class
        {
            var command = new ExecuteWithObjectResponseCommand<TResult>(_connectionString);
            return command.ExecuteAsync(storedProcedure, cancellationToken);
        }

        public Task<int> ExecuteAsync(StoredProcedure storedProcedure, CancellationToken cancellationToken)
        {
            var command = new ExecuteWithoutResponseCommand(_connectionString);
            return command.ExecuteAsync(storedProcedure, cancellationToken);
        }
    }
}
