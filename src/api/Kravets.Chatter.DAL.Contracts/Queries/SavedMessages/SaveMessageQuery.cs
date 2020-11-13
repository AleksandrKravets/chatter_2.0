namespace Kravets.Chatter.DAL.Contracts.Queries.SavedMessages
{
    /// <summary>
    /// Represents query to save message.
    /// </summary>
    public class SaveMessageQuery
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
            /// Message identifier.
            /// </summary>
            public long MessageId { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="userId">User identfier.</param>
            /// <param name="messageId">Message identifier.</param>
            public Parameters(long userId, long messageId)
            {
                UserId = userId;
                MessageId = messageId;
            }
        }
    }
}