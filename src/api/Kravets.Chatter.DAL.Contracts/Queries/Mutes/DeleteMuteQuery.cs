namespace Kravets.Chatter.DAL.Contracts.Queries.Mutes
{
    /// <summary>
    /// Represents query to delete mute.
    /// </summary>
    public class DeleteMuteQuery
    {
        /// <summary>
        /// Parameters.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// Logged user identifier.
            /// </summary>
            public long UserId { get; private set; }
            /// <summary>
            /// User to unmute identifier.
            /// </summary>
            public long UserToUnmuteId { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="userId">Logged user identifier.</param>
            /// <param name="userToUnmuteId">User to unmute identifier.</param>
            public Parameters(long userId, long userToUnmuteId)
            {
                UserId = userId;
                UserToUnmuteId = userToUnmuteId;
            }
        }
    }
}
