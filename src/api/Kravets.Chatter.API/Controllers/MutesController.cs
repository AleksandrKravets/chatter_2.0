using Kravets.Chatter.BLL.Contracts.Commands.Mutes.Create;
using Kravets.Chatter.BLL.Contracts.Commands.Mutes.Delete;
using Kravets.Chatter.BLL.Contracts.Commands.Mutes.GetList;
using Kravets.Chatter.BLL.Contracts.Models.Responses;
using Kravets.Chatter.Common.ResponseMessages;
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
    /// Represents controller to manage mutes.
    /// </summary>
    [Authorize]
    [Route("api/mutes")]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public class MutesController : ControllerBase
    {
        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public MutesController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Mutes user by identifier.
        /// </summary>
        /// <param name="id">User to mute identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpPost]
        [Route("~/api/users/{id:long}/mute")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(BLError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Mute([FromRoute] long id, CancellationToken cancellationToken)
        {
            if (WrongIdentifier(id))
                return BadRequest(ErrorMessages.WrongIdentifier);

            await _mediator.Send(new CreateMuteRequest(GetUserId(), id), cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Unmutes user by identifier.
        /// </summary>
        /// <param name="id">User to unmute identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpPost]
        [Route("~/api/users/{id:long}/unmute")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(BLError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Unmute([FromRoute] long id, CancellationToken cancellationToken)
        {
            if (WrongIdentifier(id))
                return BadRequest(ErrorMessages.WrongIdentifier);

            await _mediator.Send(new DeleteMuteRequest(GetUserId(), id), cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Returns muted users for user by user identifier.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><The collection of <see cref="MutedUserModel"></see>./returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MutedUserModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetMutedUsersByUserIdRequest(GetUserId()), cancellationToken);
            return Ok(response);
        }
    }
}
