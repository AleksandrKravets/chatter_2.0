namespace Kravets.Chatter.API.Hubs.Models.Messages
{
    /// <summary>
    /// Message typingEvent.
    /// </summary>
    public class TypingNotification
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public long UserId { get; private set; }
        /// <summary>
        /// User nickname.
        /// </summary>
        public string Nickname { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="nickname">User nickname.</param>
        public TypingNotification(long userId, string nickname)
        {
            UserId = userId;
            Nickname = nickname;
        }
    }
}
