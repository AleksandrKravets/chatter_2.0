namespace Kravets.Chatter.DAL.Contracts.Queries.Users
{
    /// <summary>
    /// Represents query to get user by identifier.
    /// </summary>
    public class GetUserByIdQuery
    {
        /// <summary>
        /// Query parameters.
        /// </summary>
        public class Parameters
        {
            public long Id { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="userId">User identifier.</param>
            public Parameters(long userId)
            {
                Id = userId;
            }
        }

        /// <summary>
        /// Query result.
        /// </summary>
        public class Result
        {
            /// <summary>
            /// User identifier.
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// User nickname.
            /// </summary>
            public string Nickname { get; set; }
            /// <summary>
            /// User email.
            /// </summary>
            public string Email { get; set; }
            /// <summary>
            /// User hashed password.
            /// </summary>
            public string HashedPassword { get; set; }
        }
    }
}
