using Kravets.Chatter.DAL.Contracts.Queries.SavedMessages;
using System;

namespace Kravets.Chatter.BLL.Contracts.Commands.SavedMessages.GetList
{
    /// <summary>
    /// Represents message model.
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// Message identifier.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Message text.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Time of message creation.
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// Boolean flag that shows if message is updated.
        /// </summary>
        public bool IsUpdated { get; set; }
        /// <summary>
        /// Boolean flag that shows if message is reply.
        /// </summary>
        public bool IsReply { get; set; }
        /// <summary>
        /// Message owner.
        /// </summary>
        public MessageOwnerModel Owner { get; set; }
        /// <summary>
        /// Message to reply data.
        /// </summary>
        public MessageToReplyData MessageToReplyData { get; set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="dto">Message dto.</param>
        /// <param name="loggedUserId">Current logged user identifier.</param>
        public MessageModel(GetSavedMessagesQuery.ResultDto dto, long loggedUserId)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            Id = dto.MessageId;
            Text = dto.Text;
            CreationTime = dto.CreationTime;
            IsUpdated = dto.IsUpdated;
            Owner = new MessageOwnerModel(dto.UserId, dto.UserNickName);
            IsReply = dto.IsReply;
            if (IsReply)
            {
                MessageToReplyData = new MessageToReplyData(
                    dto.MessageToReplyText,
                    dto.MessageToReplyOwnerId ?? -1,
                    dto.MessageToReplyOwnerNickname);
            }
        }
    }
}
