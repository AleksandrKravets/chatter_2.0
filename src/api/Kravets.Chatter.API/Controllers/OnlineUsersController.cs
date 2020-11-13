using Kravets.Chatter.BLL.Contracts.Commands.OnlineUsers.GetCount;
using Kravets.Chatter.BLL.Contracts.Commands.OnlineUsers.GetList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.API.Controllers
{
    /// <summary>
    /// Represents controller to manage online users.
    /// </summary>
    [Route("api/online-users")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public class OnlineUsersController : ControllerBase
    {
        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public OnlineUsersController(IMediator mediator) : base(mediator) {}

        /// <summary>
        /// Returns online users.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of <see cref="OnlineUserModel"></see>.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OnlineUserModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetOnlineUsersRequest(GetUserId()), cancellationToken);
            return Ok(response.Value);
        }

        /// <summary>
        /// Returns online users count.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Online users count.</returns>
        [HttpGet]
        [Route("count")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCount(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetOnlineUsersCountRequest(), cancellationToken);
            return Ok(response);
        }
    }
}
