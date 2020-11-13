using CSharpFunctionalExtensions;
using Kravets.Chatter.DAL.Contracts.Queries.Tokens;
using Kravets.Chatter.DAL.Contracts.Repositories;
using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.StoredProcedures.Tokens;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.DAL.Repositories
{
    /// <inheritdoc />
    public class TokensRepository : ITokensRepository
    {
        private readonly StoredProcedureExecutor _procedureExecutor;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="procedureExecutor">Stored procedure executor.</param>
        public TokensRepository(StoredProcedureExecutor procedureExecutor)
        {
            _procedureExecutor = procedureExecutor;
        }

        /// <inheritdoc />
        public Task UpsertAsync(UpsertTokenQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return _procedureExecutor.ExecuteAsync(
                new spUpsertToken
                {
                    CreationTime = parameters.CreationTime,
                    ExpiryTime = parameters.ExpiryTime,
                    JwtId = parameters.JwtId,
                    Token = parameters.Token,
                    UserId = parameters.UserId
                }, cancellationToken);
        }

        /// <inheritdoc / >
        public async Task<Maybe<GetTokenQuery.Result>> GetAsync(GetTokenQuery.Parameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var result = await _procedureExecutor.ExecuteWithObjectResponseAsync<GetTokenQuery.Result>(
                new spGetToken
                {
                    Token = parameters.Token
                }, cancellationToken);

            return Maybe<GetTokenQuery.Result>.From(result);
        }
    }
}