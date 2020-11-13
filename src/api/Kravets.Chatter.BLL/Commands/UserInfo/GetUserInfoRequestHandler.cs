using CSharpFunctionalExtensions;
using Kravets.Chatter.BLL.Contracts.Commands.UserInfo.GetById;
using Kravets.Chatter.DAL.Contracts.Queries.Users;
using Kravets.Chatter.DAL.Contracts.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.UserInfo
{
    /// <summary>
    /// Get user info by user identifier handler.
    /// </summary>
    public class GetUserInfoRequestHandler : IRequestHandler<GetUserInfoRequest, Maybe<UserInfoModel>>
    {
        private readonly IUsersRepository _usersRepository;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="usersRepository">Users repository provider.</param>
        public GetUserInfoRequestHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The instance of <see cref="UserInfoModel"></see>.</returns>
        public async Task<Maybe<UserInfoModel>> Handle(GetUserInfoRequest request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetAsync(new GetUserByIdQuery.Parameters(request.UserId), cancellationToken);

            if (user.HasNoValue)
                return null;

            return new UserInfoModel(user.Value.Id, user.Value.Nickname);
        }
    }
}
