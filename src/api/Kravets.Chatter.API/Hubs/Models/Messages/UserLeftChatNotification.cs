namespace Kravets.Chatter.API.Hubs.Models.Messages
{
    /// <summary>
    /// Represents notification that users receive when user leaves chat.
    /// </summary>
    public class UserLeftChatNotification
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public long Id { get; private set; }
        /// <summary>
        /// User nickname.
        /// </summary>
        public string Nickname { get; private set; }
        /// <summary>
        /// Online users count.
        /// </summary>
        public int OnlineUsersCount { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="id">User identifier.</param>
        /// <param name="nickname">User nickname.</param>
        /// <param name="onlineUsersCount">Online users count.</param>
        public UserLeftChatNotification(long id, string nickname, int onlineUsersCount)
        {
            Id = id;
            Nickname = nickname;
            OnlineUsersCount = onlineUsersCount;
        }
    }
}
