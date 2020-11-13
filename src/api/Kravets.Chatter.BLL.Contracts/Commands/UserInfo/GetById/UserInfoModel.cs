namespace Kravets.Chatter.BLL.Contracts.Commands.UserInfo.GetById
{
    /// <summary>
    /// Represents user info model.
    /// </summary>
    public class UserInfoModel
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
        public UserInfoModel(long id, string nickname)
        {
            Id = id;
            Nickname = nickname;
        }
    }
}
