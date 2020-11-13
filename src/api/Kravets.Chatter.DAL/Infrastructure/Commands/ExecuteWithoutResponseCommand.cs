using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.DAL.Infrastructure.Commands
{
    internal class ExecuteWithoutResponseCommand : CommandBase<int>
    {
        public ExecuteWithoutResponseCommand(string connectionString) : base(connectionString) { }

        protected override async Task<int> ExecuteAsync(DbCommand command, CancellationToken cancellationToken)
        {
            var rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected;
        }
    }
}
