using CSharpFunctionalExtensions;
using Kravets.Chatter.DAL.Contracts.Queries.SavedMessages;
using Kravets.Chatter.DAL.Contracts.Repositories;
using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.StoredProcedures.SavedMessages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.DAL.Repositories
{
    /// <inheritdoc />
    public class SavedMessagesRepository : ISavedMessagesRepository
    {
        private readonly StoredProcedureExecutor _procedureExecutor;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="procedureExecutor">Stored procedure executor.</param>
        public SavedMessagesRepository(StoredProcedureExecutor procedureExecutor)
        {
            _procedureExecutor = procedureExecutor;
        }

        /// <inheritdoc />
        public Task DeleteAsync(DeleteSavedMessageQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return _procedureExecutor.ExecuteAsync(
                new spDeleteSavedMessage { Id = parameters.Id }, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<GetSavedMessagesQuery.Result> GetAsync(GetSavedMessagesQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var storedProcedure = new spGetSavedMessages
            {
                PageSize = parameters.PageSize,
                PageIndex = parameters.PageIndex,
                UserId = parameters.UserId
            };

            var response = await _procedureExecutor.ExecuteWithListResponseAsync<GetSavedMessagesQuery.ResultDto>(
                    storedProcedure, cancellationToken);

            var result = new GetSavedMessagesQuery.Result(response, storedProcedure.NextPageExists);

            return result;
        }

        /// <inheritdoc />
        public async Task<Maybe<GetSavedMessageQuery.Result>> GetAsync(GetSavedMessageQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var result = await _procedureExecutor.ExecuteWithObjectResponseAsync<GetSavedMessageQuery.Result>(
                new spGetSavedMessage { Id = parameters.Id }, cancellationToken);

            return Maybe<GetSavedMessageQuery.Result>.From(result);
        }

        /// <inheritdoc />
        public Task SaveAsync(SaveMessageQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return _procedureExecutor.ExecuteAsync(
                new spCreateSavedMessage 
                { 
                    MessageId = parameters.MessageId, 
                    UserId = parameters.UserId    
                }, cancellationToken);
        }
    }
}
