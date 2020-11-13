using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Kravets.Chatter.DAL.Infrastructure
{
    internal class SqlStoredProcedureCommandBuilder
    {
        private readonly SqlCommand _command;

        public SqlStoredProcedureCommandBuilder()
        {
            _command = new SqlCommand();
        }

        public SqlStoredProcedureCommandBuilder WithProcedureName(string procedureName)
        {
            _command.CommandText = procedureName;
            return this;
        }

        public SqlStoredProcedureCommandBuilder WithConnection(SqlConnection connection)
        {
            _command.Connection = connection;
            return this;
        }

        public SqlStoredProcedureCommandBuilder WithParameters(params StoredProcedureParameter[] parameters)
        {
            _command.Parameters.AddRange(parameters
                .Select(p => new SqlParameter
                {
                    ParameterName = p.Name,
                    Value = p.Value,
                    Direction = p.Direction
                })
                .ToArray());

            return this;
        }

        public SqlCommand Build()
        {
            _command.CommandType = CommandType.StoredProcedure;
            return _command;
        }
    }
}
