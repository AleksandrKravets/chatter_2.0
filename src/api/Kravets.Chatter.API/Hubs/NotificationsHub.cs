using Kravets.Chatter.API.Extensions;
using Kravets.Chatter.API.Hubs.Contracts;
using Kravets.Chatter.API.Hubs.Models.Messages;
using Kravets.Chatter.BLL.Contracts.Models.OnlineUsers;
using Kravets.Chatter.BLL.Contracts.Services;
using Kravets.Chatter.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Kravets.Chatter.API.Hubs
{
    /// <summary>
    /// Represents notification hub.
    /// </summary>
    [Authorize]
    public class NotificationsHub : Hub<INotificationsHub>
    {
        private readonly IOnlineUsersService _onlineUsersService;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="onlineUsersService">Online users service.</param>
        public NotificationsHub(IOnlineUsersService onlineUsersService)
        {
            _onlineUsersService = onlineUsersService;
        }

        /// <summary>
        /// Method that calls when new user joines chat.
        /// </summary>
        public override Task OnConnectedAsync()
        {
            var user = new OnlineUserModel(GetUserId(), GetUserNickname());
            _onlineUsersService.Add(user);
            var notification = new UserJoinedChatNotification(user.Id, user.Nickname, _onlineUsersService.Count);
            var userJoinedTask =  Clients.Others.UserJoinedChat(notification);
            var onConnectedTask = base.OnConnectedAsync();
            return Task.WhenAll(userJoinedTask, onConnectedTask);
        }

        /// <summary>
        /// Method that calls when user leaves chat.
        /// </summary>
        /// <param name="exception">Exception model.</param>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            _onlineUsersService.Delete(GetUserId());
            var notification = new UserLeftChatNotification(GetUserId(), GetUserNickname(), _onlineUsersService.Count);
            var userLeftTask = Clients.Others.UserLeftChat(notification);
            var onDisconnectedTask = base.OnDisconnectedAsync(exception);
            return Task.WhenAll(userLeftTask, onDisconnectedTask);
        }

        /// <summary>
        /// Method that calls when user is typing.
        /// </summary>
        public Task MessageTyping()
        {
            var notification = new TypingNotification(GetUserId(), GetUserNickname());
            return Clients.Others.Typing(notification);
        }

        protected long GetUserId() => Context.User.GetLongClaim(CustomClaims.UserId);
        protected string GetUserNickname() => Context.User.GetStringClaim(CustomClaims.Nickname);
    }
}
