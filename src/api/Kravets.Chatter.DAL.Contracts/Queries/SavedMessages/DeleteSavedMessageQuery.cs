namespace Kravets.Chatter.DAL.Contracts.Queries.SavedMessages
{
    /// <summary>
    /// Represents query to delete saved message.
    /// </summary>
    public class DeleteSavedMessageQuery
    {
        /// <summary>
        /// Parameters.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// Saved message identifier.
            /// </summary>
            public long Id { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="id">Saved message identfier.</param>
            public Parameters(long id)
            {
                Id = id;
            }
        }
    }
}
