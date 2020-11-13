namespace Kravets.Chatter.BLL.Contracts.Commands.OnlineUsers.GetList
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
        /// Is muted for current logged user.
        /// </summary>
        public bool IsMuted { get; private set; }
        /// <summary>
        /// Is current user.
        /// </summary>
        public bool IsCurrentUser { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="id">User identifier.</param>
        /// <param name="nickname">User nickname.</param>
        /// <param name="isMuted">Is muted for current logged user.</param>
        /// <param name="isCurrentUser">Is current user.</param>
        public OnlineUserModel(long id, string nickname, bool isMuted, bool isCurrentUser) =>
            (Id, Nickname, IsMuted, IsCurrentUser) = (id, nickname, isMuted, isCurrentUser);
    }
}