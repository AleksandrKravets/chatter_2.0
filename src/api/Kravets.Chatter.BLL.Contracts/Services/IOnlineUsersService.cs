using Kravets.Chatter.BLL.Contracts.Models.OnlineUsers;
using System.Collections.Generic;

namespace Kravets.Chatter.BLL.Contracts.Services
{
    /// <summary>
    /// Represents service to manage online users.
    /// </summary>
    public interface IOnlineUsersService
    {
        /// <summary>
        /// Returns count of online users.
        /// </summary>
        int Count { get; }
        /// <summary>
        /// Returns a collection of online users.
        /// </summary>
        /// <returns></returns>
        IEnumerable<OnlineUserModel> Get();
        /// <summary>
        /// Add new online user to collection. This method should be called when user
        /// joins chat.
        /// </summary>
        /// <param name="user">User model.</param>
        void Add(OnlineUserModel user);
        /// <summary>
        /// Deletes online user from collection. This method should be called when user
        /// leaves chat.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        bool Delete(long userId);
    }
}
