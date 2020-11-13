using CSharpFunctionalExtensions;
using Kravets.Chatter.DAL.Contracts.Queries.SavedMessages;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.DAL.Contracts.Repositories
{
    /// <summary>
    /// Represents repository to manage saved messages.
    /// </summary>
    public interface ISavedMessagesRepository
    {
        /// <summary>
        /// Returns saved messages for user.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The colletion of <see cref="GetSavedMessagesQuery.Result"></see>.</returns>
        Task<GetSavedMessagesQuery.Result> GetAsync(
            GetSavedMessagesQuery.Parameters parameters, 
            CancellationToken cancellationToken);

        /// <summary>
        /// Saves message.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task SaveAsync(SaveMessageQuery.Parameters parameters, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes message.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task DeleteAsync(DeleteSavedMessageQuery.Parameters parameters, CancellationToken cancellationToken);

        /// <summary>
        /// Returns saved message by identifier.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The instance of <see cref="GetSavedMessageQuery.Result"></see>.</returns>
        Task<Maybe<GetSavedMessageQuery.Result>> GetAsync(
            GetSavedMessageQuery.Parameters parameters, 
            CancellationToken cancellationToken);
    }
}
