using Kravets.Chatter.BLL.Contracts.Models.OnlineUsers;
using Kravets.Chatter.BLL.Contracts.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Kravets.Chatter.BLL.Services
{
    /// <inheritdoc />
    public class OnlineUsersService : IOnlineUsersService
    {
        private readonly ConcurrentDictionary<long, OnlineUserModel> _users;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        public OnlineUsersService() => (_users) = (new ConcurrentDictionary<long, OnlineUserModel>());

        /// <inheritdoc />
        public int Count => _users.Count();

        /// <inheritdoc />
        public void Add(OnlineUserModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if(!_users.Keys.Any(x => x == user.Id))
            {
                _users.TryAdd(user.Id, user);
            }
        }

        /// <inheritdoc />
        public bool Delete(long userId) => _users.TryRemove(userId, out _);

        /// <inheritdoc />
        public IEnumerable<OnlineUserModel> Get() => _users.Values;
    }
}
