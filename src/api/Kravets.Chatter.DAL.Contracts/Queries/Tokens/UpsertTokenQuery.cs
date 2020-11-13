using System;

namespace Kravets.Chatter.DAL.Contracts.Queries.Tokens
{
    /// <summary>
    /// Represents query to upsert refresh token.
    /// </summary>
    public class UpsertTokenQuery
    {
        /// <summary>
        /// Parameters.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// JWT identifier.
            /// </summary>
            public string JwtId { get; set; }
            /// <summary>
            /// Time of creation.
            /// </summary>
            public DateTime CreationTime { get; set; }
            /// <summary>
            /// Time of expiration.
            /// </summary>
            public DateTime ExpiryTime { get; set; }
            /// <summary>
            /// Refresh token.
            /// </summary>
            public string Token { get; set; }
            /// <summary>
            /// User identifier.
            /// </summary>
            public long UserId { get; set; }

            public Parameters(string jwtId, DateTime creationTime, DateTime expiryTime, string token, long userId) =>
                (JwtId, CreationTime, ExpiryTime, Token, UserId) = (jwtId, creationTime, expiryTime, token, userId);
        }
    }
}
