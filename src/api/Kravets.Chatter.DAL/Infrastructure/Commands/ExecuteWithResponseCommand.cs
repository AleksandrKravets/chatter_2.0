using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.DAL.Infrastructure.Commands
{
    internal abstract class ExecuteWithResponseCommand<TResult> : CommandBase<TResult> where TResult : class
    {
        public ExecuteWithResponseCommand(string connectionString) : base(connectionString) { }

        protected override async Task<TResult> ExecuteAsync(DbCommand command, CancellationToken cancellationToken)
        {
            TResult result;

            using (var reader = await command.ExecuteReaderAsync(cancellationToken))
            {
                if (!reader.HasRows)
                    return null;

                result = Execute(reader);
                reader.Close();
            }

            return result;
        }

        protected abstract TResult Execute(DbDataReader reader);
    }
}
