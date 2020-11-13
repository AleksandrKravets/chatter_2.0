namespace Kravets.Chatter.DAL.Contracts.Queries.Mutes
{
    /// <summary>
    /// Represents query to get muted users for user by user identifier.
    /// </summary>
    public class GetMutedUsersByUserIdQuery
    {
        /// <summary>
        /// Parameters.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// User identifier.
            /// </summary>
            public long UserId { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="userId">User identifier.</param>
            public Parameters(long userId) =>
                (UserId) = (userId);
        }

        /// <summary>
        /// Result.
        /// </summary>
        public class Result
        {
            /// <summary>
            /// Mute identifier.
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// Muted user identifier.
            /// </summary>
            public long MutedUserId { get; set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="id">Mute identifier.</param>
            /// <param name="mutedUserId">Muted user identifier.</param>
            public Result(long id, long mutedUserId)
            {
                Id = id;
                MutedUserId = mutedUserId;
            }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            public Result() { }
        }
    }
}
