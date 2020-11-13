using CSharpFunctionalExtensions;
using Kravets.Chatter.DAL.Contracts.Queries.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.DAL.Contracts.Repositories
{
    /// <summary>
    /// Represents repository to manage users.
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// Returns user by identifier.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The instance of <see cref="GetUserByIdQuery.Result"></see> or null.</returns>
        Task<Maybe<GetUserByIdQuery.Result>> GetAsync(GetUserByIdQuery.Parameters parameters, CancellationToken cancellationToken);

        /// <summary>
        /// Returns user by nickname.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The instance of <see cref="GetUserByIdQuery.Result"></see> or null.</returns>
        Task<Maybe<GetUserByNicknameQuery.Result>> GetAsync(GetUserByNicknameQuery.Parameters parameters, CancellationToken cancellationToken);

        /// <summary>
        /// Creates user.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task CreateAsync(CreateUserQuery.Parameters parameters, CancellationToken cancellationToken);
    }
}