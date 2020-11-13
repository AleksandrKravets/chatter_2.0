namespace Kravets.Chatter.DAL.Contracts.Queries.Messages
{
    /// <summary>
    /// Represents query to update message.
    /// </summary>
    public class UpdateMessageQuery
    {
        /// <summary>
        /// Parameters.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// Message identifier.
            /// </summary>
            public long Id { get; private set; }
            /// <summary>
            /// Message text.
            /// </summary>
            public string Text { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="id">Message identifier.</param>
            /// <param name="text">Message text.</param>
            public Parameters(long id, string text) => (Id, Text) = (id, text);
        }
    }
}
