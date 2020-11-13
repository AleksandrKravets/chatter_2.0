using Kravets.Chatter.API.Models.SavedMessages;
using Kravets.Chatter.BLL.Contracts.Commands.SavedMessages.Delete;
using Kravets.Chatter.BLL.Contracts.Commands.SavedMessages.GetList;
using Kravets.Chatter.BLL.Contracts.Commands.SavedMessages.Save;
using Kravets.Chatter.BLL.Contracts.Models.Collections;
using Kravets.Chatter.BLL.Contracts.Models.Responses;
using Kravets.Chatter.Common.ResponseMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.API.Controllers
{
    /// <summary>
    /// Represents controller to manage saved messages.
    /// </summary>
    [Route("api/saved-messages")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public class SavedMessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        /// <param name="logger">Logging provider.</param>
        public SavedMessagesController(
            IMediator mediator,
            ILogger<MessagesController> logger) : base(mediator)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns user saved messages.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of saved messages.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedListModel<SavedMessageModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery]GetSavedMessagesFilter filter, CancellationToken cancellationToken)
        {
            if (filter is null)
                filter = new GetSavedMessagesFilter();

            var result = await _mediator.Send(
                new GetSavedMessagesRequest(filter.PageIndex, filter.PageSize, GetUserId()), cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Deletes saved message by identifier.
        /// </summary>
        /// <param name="id">Message identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpDelete]
        [Route("{id:long}")]
        [ProducesResponseType(typeof(BLError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
        {
            if (WrongIdentifier(id))
                return BadRequest(ErrorMessages.WrongIdentifier);

            var response = await _mediator.Send(new DeleteSavedMessageRequest(id, GetUserId()), cancellationToken);

            var result = response.Match(
                success => NoContent(),
                blError => ProcessBLError(blError, _logger));

            return result;
        }

        /// <summary>
        /// Saves message.
        /// </summary>
        /// <param name="model">Message model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpPost]
        [ProducesResponseType(typeof(BLError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Save([FromBody] SaveMessageModel model, CancellationToken cancellationToken)
        {
            if (model is null)
                return BadRequest(ErrorMessages.RequiredBody);

            var response = await _mediator.Send(new SaveMessageRequest(model.MessageId, GetUserId()), cancellationToken);

            var result = response.Match(
                success => NoContent(),
                blError => ProcessBLError(blError, _logger));

            return result;
        }
    }
}
