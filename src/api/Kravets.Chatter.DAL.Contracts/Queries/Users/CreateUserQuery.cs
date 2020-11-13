namespace Kravets.Chatter.DAL.Contracts.Queries.Users
{
    /// <summary>
    /// Represents query to create user.
    /// </summary>
    public class CreateUserQuery
    {
        /// <summary>
        /// Parameters.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// User nickname.
            /// </summary>
            public string Nickname { get; set; }
            /// <summary>
            /// Hashed password.
            /// </summary>
            public string HashedPassword { get; set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="nickname">User nickname.</param>
            /// <param name="hashedPassword">Hashed password.</param>
            public Parameters(string nickname, string hashedPassword) =>
                (Nickname, HashedPassword) = (nickname, hashedPassword);
        }
    }
}
