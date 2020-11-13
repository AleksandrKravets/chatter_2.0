using System;

namespace Kravets.Chatter.DAL.Contracts.Queries.Tokens
{
    /// <summary>
    /// Represents query to get refresh token.
    /// </summary>
    public class GetTokenQuery
    {
        /// <summary>
        /// Parameters.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// Refresh token.
            /// </summary>
            public string Token { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="token">Refresh token.</param>
            public Parameters(string token) =>
                (Token) = (token);
        }

        /// <summary>
        /// Result.
        /// </summary>
        public class Result
        {
            /// <summary>
            /// Refresh token.
            /// </summary>
            public string Token { get; set; }
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
            /// Token owner identifier.
            /// </summary>
            public long UserId { get; set; }
        }
    }
}
