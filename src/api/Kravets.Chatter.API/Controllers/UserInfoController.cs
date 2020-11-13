using Kravets.Chatter.BLL.Contracts.Commands.UserInfo.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.API.Controllers
{
    /// <summary>
    /// Represents controller to manage users accounts.
    /// </summary>
    [Route("api/user-info")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public class UserInfoController : ControllerBase
    {
        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="mediator">Mediator provider.</param>
        public UserInfoController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Returns user info model.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The instance of <see cref="UserInfoModel"></see>.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserInfoModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUserInfoRequest(GetUserId()), cancellationToken);
            return Ok(response.Value);
        }
    }
}
