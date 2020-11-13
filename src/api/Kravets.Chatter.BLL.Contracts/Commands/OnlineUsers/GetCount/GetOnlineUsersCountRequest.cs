using MediatR;

namespace Kravets.Chatter.BLL.Contracts.Commands.OnlineUsers.GetCount
{
    /// <summary>
    /// Represents request to get count of online users.
    /// </summary>
    public class GetOnlineUsersCountRequest : IRequest<int>
    {
    }
}
