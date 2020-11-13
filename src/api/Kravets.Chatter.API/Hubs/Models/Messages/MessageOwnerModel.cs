namespace Kravets.Chatter.API.Hubs.Models.Messages
{
    /// <summary>
    /// Represents message owner.
    /// </summary>
    public class MessageOwnerModel
    {
        /// <summary>
        /// Message owner identifier.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Message owner nickname.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="id">User identifier.</param>
        /// <param name="nickname">User nickname.</param>
        public MessageOwnerModel(long id, string nickname) => (Id, Nickname) = (id, nickname);
    }
}
