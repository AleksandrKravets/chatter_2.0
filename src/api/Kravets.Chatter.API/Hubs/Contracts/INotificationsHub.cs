using Kravets.Chatter.API.Hubs.Models.Messages;
using System.Threading.Tasks;

namespace Kravets.Chatter.API.Hubs.Contracts
{
    /// <summary>
    /// Represents notification hub.
    /// </summary>
    public interface INotificationsHub
    {
        /// <summary>
        /// Sends notification that message has been deleted.
        /// </summary>
        /// <param name="id">Deleted message identifier.</param>
        Task Delete(long id);

        /// <summary>
        /// Sends notification that new message has been created.
        /// </summary>
        /// <param name="notification">Notification model.</param>
        Task Create(CreateMessageNotification notification);

        /// <summary>
        /// Sends notification that message has been updated.
        /// </summary>
        /// <param name="notification">Notification model.</param>
        Task Update(UpdateMessageNotification notification);

        /// <summary>
        /// Sends notification when new user joines chat. 
        /// </summary>
        /// <param name="notification">Notification model.</param>
        Task UserJoinedChat(UserJoinedChatNotification notification);

        /// <summary>
        /// Sends notification when user leaves chat.
        /// </summary>
        /// <param name="notification">Notification model.</param>
        Task UserLeftChat(UserLeftChatNotification notification);

        /// <summary>
        /// Sends notification when someone is typing.
        /// </summary>
        /// <param name="notification">Notification model.</param>
        Task Typing(TypingNotification notification);
    }
}
