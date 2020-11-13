namespace Kravets.Chatter.API.Hubs.Models.Messages
{
    /// <summary>
    /// Represents notificatin that sends when user updates message.
    /// </summary>
    public class UpdateMessageNotification
    {
        /// <summary>
        /// Updated message identifier.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Updated message text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="id">Updated message identifier.</param>
        /// <param name="text">Updated message text.</param>
        public UpdateMessageNotification(long id, string text) => (Id, Text) = (id, text);
    }
}
