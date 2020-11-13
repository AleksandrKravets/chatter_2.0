using CSharpFunctionalExtensions;
using MediatR;

namespace Kravets.Chatter.BLL.Contracts.Commands.UserInfo.GetById
{
    /// <summary>
    /// Represents request to get user info by user identifier.
    /// </summary>
    public class GetUserInfoRequest : IRequest<Maybe<UserInfoModel>>
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        public GetUserInfoRequest(long userId) =>
            (UserId) = (userId);
    }
}
