namespace Kravets.Chatter.BLL.Contracts.Commands.SavedMessages.GetList
{
    /// <summary>
    /// Message to reply data.
    /// </summary>
    public class MessageToReplyData
    {
        /// <summary>
        /// Text.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Owner identifier.
        /// </summary>
        public long OwnerId { get; set; }
        /// <summary>
        /// Owner nickname.
        /// </summary>
        public string OwnerNickname { get; set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="ownerId">Owner identifier.</param>
        /// <param name="ownerNickname">Owner nickname.</param>
        public MessageToReplyData(string text, long ownerId, string ownerNickname)
        {
            Text = text;
            OwnerId = ownerId;
            OwnerNickname = ownerNickname;
        }
    }
}
