using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.DAL.Infrastructure.Commands
{
    internal abstract class CommandBase<TResult>
    {
        private readonly string _connectionString;

        public CommandBase(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
        }

        protected abstract Task<TResult> ExecuteAsync(DbCommand command, CancellationToken cancellationToken);

        public async Task<TResult> ExecuteAsync(StoredProcedure storedProcedure, CancellationToken cancellationToken)
        {
            if (storedProcedure == null)
                throw new ArgumentNullException(nameof(storedProcedure));

            TResult result;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                DbCommand _command = BuildCommand(connection, storedProcedure);

                result = await ExecuteAsync(_command, cancellationToken);

                FillProcedureWithOutParameters(storedProcedure, _command);
                FillProcedureWithReturnValue(storedProcedure, _command);
            }

            return result;
        }

        private DbCommand BuildCommand(SqlConnection connection, StoredProcedure storedProcedure)
        {
            var commandBuilder = new SqlStoredProcedureCommandBuilder();

            SqlCommand command = commandBuilder
                .WithConnection(connection)
                .WithProcedureName(storedProcedure.GetName())
                .WithParameters(storedProcedure.GetParameters())
                .Build();

            return command;
        }
        private void FillProcedureWithReturnValue(StoredProcedure storedProcedure, DbCommand command)
        {
            var returnField = storedProcedure.GetReturnField();

            if (returnField != null)
            {
                Reflector.SetFieldValue(
                    obj: storedProcedure, 
                    fieldName: returnField.Name, 
                    value: Convert.ChangeType(command.Parameters[$"{returnField.Name}"].Value, returnField.FieldType));
            }
        }

        private void FillProcedureWithOutParameters(StoredProcedure storedProcedure, DbCommand command)
        {
            var outFields = storedProcedure.GetOutFields();

            foreach (var field in outFields)
            {
                Reflector.SetFieldValue(
                    obj: storedProcedure, 
                    fieldName: field.Name,
                    value: Convert.ChangeType(command.Parameters[$"{field.Name}"].Value, field.FieldType));
            }
        }
    }
}
