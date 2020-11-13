namespace Kravets.Chatter.DAL.Contracts.Queries.Mutes
{
    /// <summary>
    /// Represents query to create mute.
    /// </summary>
    public class CreateMuteQuery
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
            /// User to mute identifier.
            /// </summary>
            public long UserToMuteId { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="userId">Logged user identifier.</param>
            /// <param name="userToMuteId">User to mute identifier.</param>
            public Parameters(long userId, long userToMuteId)
            {
                UserId = userId;
                UserToMuteId = userToMuteId;
            }
        }
    }
}
