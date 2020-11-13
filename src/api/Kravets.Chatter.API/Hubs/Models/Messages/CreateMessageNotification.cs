using Kravets.Chatter.BLL.Contracts.Commands.Messages.Create;
using System;

namespace Kravets.Chatter.API.Hubs.Models.Messages
{
    /// <summary>
    /// Represents notificatin that sends when user creates message.
    /// </summary>
    public class CreateMessageNotification
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
        /// <param name="model">Created message model.</param>
        public CreateMessageNotification(CreatedMessageModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            Id = model.Id;
            Text = model.Text;
            CreationTime = model.CreationTime;
            IsUpdated = model.IsUpdated;
            Owner = new MessageOwnerModel(model.Owner.Id, model.Owner.Nickname);
            IsReply = model.IsReply;
            if (IsReply)
            {
                MessageToReplyData = new MessageToReplyData(
                    model.MessageToReplyData.Text,
                    model.MessageToReplyData.OwnerId,
                    model.MessageToReplyData.OwnerNickname);
            }
        }
    }
}
