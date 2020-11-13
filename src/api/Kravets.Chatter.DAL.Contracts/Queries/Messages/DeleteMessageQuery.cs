namespace Kravets.Chatter.DAL.Contracts.Queries.Messages
{
    /// <summary>
    /// Represents query to delete message by message identifier.
    /// </summary>
    public class DeleteMessageQuery
    {
        /// <summary>
        /// Query parameters.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// Message to delete identifier.
            /// </summary>
            public long MessageId { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="messageId">Message identifier.</param>
            public Parameters(long messageId) =>
                (MessageId) = (messageId);
        }
    }
}
