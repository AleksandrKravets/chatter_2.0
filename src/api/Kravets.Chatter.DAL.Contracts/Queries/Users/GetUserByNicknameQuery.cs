namespace Kravets.Chatter.DAL.Contracts.Queries.Users
{
    /// <summary>
    /// Represents query to get user by nickname.
    /// </summary>
    public class GetUserByNicknameQuery
    {
        /// <summary>
        /// Parameters.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// User nickname.
            /// </summary>
            public string Nickname { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="nickname">User nickname.</param>
            public Parameters(string nickname) => (Nickname) = (nickname);
        }

        /// <summary>
        /// Result.
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
