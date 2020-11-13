using CSharpFunctionalExtensions;
using Kravets.Chatter.DAL.Contracts.Queries.Messages;
using Kravets.Chatter.DAL.Contracts.Repositories;
using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.StoredProcedures.Messages;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.DAL.Repositories
{
    /// <inheritdoc />
    public class MessagesRepository : IMessagesRepository
    {
        private readonly StoredProcedureExecutor _procedureExecutor;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="procedureExecutor">Stored procedure executor.</param>
        public MessagesRepository(StoredProcedureExecutor procedureExecutor)
        {
            _procedureExecutor = procedureExecutor;
        }

        /// <inheritdoc />
        public async Task<CreateMessageQuery.Result> CreateAsync(CreateMessageQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var storedProcedure = new spCreateMessage
            {
                CreationTime = parameters.CreationTime,
                UserId = parameters.UserId,
                IsReply = parameters.IsReply,
                MessageToReplyId = parameters.MessageToReplyId,
                Text = parameters.Text
            };

            await _procedureExecutor.ExecuteAsync(storedProcedure, cancellationToken);

            return new CreateMessageQuery.Result(storedProcedure.NewMessageId);
        }

        /// <inheritdoc />
        public Task DeleteAsync(DeleteMessageQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            _procedureExecutor.ExecuteAsync(
                new spDeleteMessage { Id = parameters.MessageId }, cancellationToken);

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public async Task<GetMessagesQuery.Result> GetAsync(GetMessagesQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var storedProcedure = new spGetMessages
            {
                LastMessageId = parameters.LastMessageId,
                PageSize = parameters.PageSize
            };

            var response = await _procedureExecutor.ExecuteWithListResponseAsync<GetMessagesQuery.ResultDto>(
                    storedProcedure, cancellationToken);


            var result = new GetMessagesQuery.Result(response, storedProcedure.NextPageExists);

            return result;
        }

        /// <inheritdoc />
        public async Task<Maybe<GetMessageByIdQuery.Result>> GetAsync(GetMessageByIdQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var result = await _procedureExecutor.ExecuteWithObjectResponseAsync<GetMessageByIdQuery.Result>(
                new spGetMessageById
                {
                    Id = parameters.Id
                }, cancellationToken);

            return Maybe<GetMessageByIdQuery.Result>.From(result);
        }

        /// <inheritdoc />
        public Task<IEnumerable<GetMessagesFromIdQuery.Result>> GetFromIdAsync(GetMessagesFromIdQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return _procedureExecutor.ExecuteWithListResponseAsync<GetMessagesFromIdQuery.Result>(
                new spGetMessagesFromId
                {
                    Id = parameters.Id
                }, cancellationToken);
        }

        /// <inheritdoc />
        public Task UpdateAsync(UpdateMessageQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return _procedureExecutor.ExecuteAsync(
                new spUpdateMessage
                {
                    Id = parameters.Id,
                    Text = parameters.Text
                }, cancellationToken);
        }
    }
}
