namespace Kravets.Chatter.BLL.Contracts.Commands.Mutes.GetList
{
    /// <summary>
    /// Represents muted user model.
    /// </summary>
    public class MutedUserModel
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="id">User identifier.</param>
        public MutedUserModel(long id) => (Id) = (id);
    }
}
