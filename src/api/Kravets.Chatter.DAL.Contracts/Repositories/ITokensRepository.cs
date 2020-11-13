using CSharpFunctionalExtensions;
using Kravets.Chatter.DAL.Contracts.Queries.Tokens;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.DAL.Contracts.Repositories
{
    /// <summary>
    /// Represents repository to manage tokens.
    /// </summary>
    public interface ITokensRepository
    {
        /// <summary>
        /// Upserts refresh token.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task UpsertAsync(UpsertTokenQuery.Parameters parameters, CancellationToken cancellationToken);

        /// <summary>
        /// Returns refresh token by token.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The instance of <see cref="GetTokenQuery.Result"></see> or null.</returns>
        Task<Maybe<GetTokenQuery.Result>> GetAsync(GetTokenQuery.Parameters parameters, CancellationToken cancellationToken);
    }
}
