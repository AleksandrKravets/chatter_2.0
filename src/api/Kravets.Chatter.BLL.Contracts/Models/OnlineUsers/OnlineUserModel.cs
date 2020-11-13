namespace Kravets.Chatter.BLL.Contracts.Models.OnlineUsers
{
    /// <summary>
    /// Represents online user model.
    /// </summary>
    public class OnlineUserModel
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
        /// Initializes instance.
        /// </summary>
        /// <param name="id">User identifier.</param>
        /// <param name="nickname">User nickname.</param>
        public OnlineUserModel(long id, string nickname)
        {
            Id = id;
            Nickname = nickname;
        }
    }
}
