using CSharpFunctionalExtensions;
using Kravets.Chatter.DAL.Contracts.Queries.Users;
using Kravets.Chatter.DAL.Contracts.Repositories;
using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.StoredProcedures.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.DAL.Repositories
{
    /// <inheritdoc />
    public class UsersRepository : IUsersRepository
    {
        private readonly StoredProcedureExecutor _procedureExecutor;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="procedureExecutor">Stored procedure executor.</param>
        public UsersRepository(StoredProcedureExecutor procedureExecutor)
        {
            _procedureExecutor = procedureExecutor;
        }

        /// <inheritdoc />
        public Task CreateAsync(CreateUserQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return _procedureExecutor.ExecuteAsync(
                new spCreateUser
                {
                    HashedPassword = parameters.HashedPassword,
                    Nickname = parameters.Nickname
                }, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Maybe<GetUserByIdQuery.Result>> GetAsync(GetUserByIdQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var result = await _procedureExecutor.ExecuteWithObjectResponseAsync<GetUserByIdQuery.Result>(
                new spGetUserById
                {
                    Id = parameters.Id
                }, cancellationToken);

            return Maybe<GetUserByIdQuery.Result>.From(result);
        }

        /// <inheritdoc />
        public async Task<Maybe<GetUserByNicknameQuery.Result>> GetAsync(GetUserByNicknameQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var result = await _procedureExecutor.ExecuteWithObjectResponseAsync<GetUserByNicknameQuery.Result>(
                new spGetUserByNickname
                {
                    Nickname = parameters.Nickname
                }, cancellationToken);

            return Maybe<GetUserByNicknameQuery.Result>.From(result);
        }
    }  
}