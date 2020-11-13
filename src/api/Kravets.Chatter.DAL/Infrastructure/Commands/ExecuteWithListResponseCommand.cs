using Kravets.Chatter.DAL.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Data.Common;

namespace Kravets.Chatter.DAL.Infrastructure.Commands
{
    internal class ExecuteWithListResponseCommand<TResult> : ExecuteWithResponseCommand<IEnumerable<TResult>> where TResult : class
    {
        public ExecuteWithListResponseCommand(string connectionString) : base(connectionString) { }

        protected override IEnumerable<TResult> Execute(DbDataReader reader)
        {
            var result = reader.ReadList<TResult>();
            return result;
        }
    }
}
