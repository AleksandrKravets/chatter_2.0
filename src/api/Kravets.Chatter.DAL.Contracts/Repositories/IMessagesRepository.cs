using CSharpFunctionalExtensions;
using Kravets.Chatter.DAL.Contracts.Queries.Messages;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.DAL.Contracts.Repositories
{
    /// <summary>
    /// Represents repository to manage messages.
    /// </summary>
    public interface IMessagesRepository
    {
        /// <summary>
        /// Returns a collection of messages.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of <see cref="GetMessagesQuery.Result"></see>.</returns>
        Task<GetMessagesQuery.Result> GetAsync(
            GetMessagesQuery.Parameters parameters,
            CancellationToken cancellationToken);

        /// <summary>
        /// Returns message by identifier.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The instance of <see cref="GetMessageByIdQuery.Result"></see>.</returns>
        Task<Maybe<GetMessageByIdQuery.Result>> GetAsync(
            GetMessageByIdQuery.Parameters parameters, 
            CancellationToken cancellationToken);

        /// <summary>
        /// Deletes message by identifier.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task DeleteAsync(DeleteMessageQuery.Parameters parameters, CancellationToken cancellationToken);

        /// <summary>
        /// Creates new message.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The instance of <see cref="CreateMessageQuery.Result"></see>.</returns>
        Task<CreateMessageQuery.Result> CreateAsync(
            CreateMessageQuery.Parameters parameters, 
            CancellationToken cancellationToken);

        /// <summary>
        /// Updates message.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task UpdateAsync(UpdateMessageQuery.Parameters parameters, CancellationToken cancellationToken);

        /// <summary>
        /// Returns messages with Id greater than the Id from parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The collection of <see cref="GetMessagesFromIdQuery.Result"></see>.</returns>
        Task<IEnumerable<GetMessagesFromIdQuery.Result>> GetFromIdAsync(
            GetMessagesFromIdQuery.Parameters parameters, 
            CancellationToken cancellationToken);
    }
}
