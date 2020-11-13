using Kravets.Chatter.API.BackgroundServices;
using Kravets.Chatter.API.BackgroundServices.Models.DelayedMessageDeletion;
using Kravets.Chatter.API.Hubs;
using Kravets.Chatter.API.Hubs.Contracts;
using Kravets.Chatter.API.Hubs.Models.Messages;
using Kravets.Chatter.API.Models.Messages;
using Kravets.Chatter.BLL.Contracts.Commands.Messages.Create;
using Kravets.Chatter.BLL.Contracts.Commands.Messages.Delete;
using Kravets.Chatter.BLL.Contracts.Commands.Messages.GetFromId;
using Kravets.Chatter.BLL.Contracts.Commands.Messages.GetList;
using Kravets.Chatter.BLL.Contracts.Commands.Messages.Update;
using Kravets.Chatter.BLL.Contracts.Models.Collections;
using Kravets.Chatter.BLL.Contracts.Models.Responses;
using Kravets.Chatter.BLL.Extensions;
using Kravets.Chatter.Common.ResponseMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Kravets.Chatter.API.Controllers
{
    /// <summary>
    /// Represents controller to manage messages.
    /// </summary>
    [Route("api/messages")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public class MessagesController : ControllerBase
    {
        private readonly IHubContext<NotificationsHub, INotificationsHub> _hubContext;
        private readonly ILogger<MessagesController> _logger;
        private readonly Channel<DelayedMessageDeletingTask> _channel;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        /// <param name="hubContext">Notification hub context.</param>
        /// <param name="logger">Logging provider.</param>
        /// <param name="channel">Delayed message deletion channel.</param>
        public MessagesController(
            IMediator mediator,
            IHubContext<NotificationsHub, INotificationsHub> hubContext,
            ILogger<MessagesController> logger, 
            Channel<DelayedMessageDeletingTask> channel) : base(mediator)
        {
            _hubContext = hubContext;
            _logger = logger;
            _channel = channel;
        }

        // Test endpoints for message deletion with delay

        //[HttpDelete]
        //[Route("delete/{id:long}")]
        //public async Task<IActionResult> Delete2([FromRoute] long id, CancellationToken cancellationToken)
        //{
        //    var key = Guid.NewGuid();
        //    await _channel.Writer.WriteAsync(new DelayedMessageDeletingTask(key, id, GetUserId(), ActionType.Delete), cancellationToken);
        //    return Ok(key);
        //}

        //[HttpDelete]
        //[Route("delete/cancel/{key:guid}")]
        //public async Task<IActionResult> CancelDeletion([FromRoute] Guid key, CancellationToken cancellationToken)
        //{
        //    await _channel.Writer.WriteAsync(new DelayedMessageDeletingTask(key, 0, GetUserId(), ActionType.CancelDeletion), cancellationToken);
        //    return Ok();
        //}

        /// <summary>
        /// Returns messages with Id greater than the Id from parameters.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of lost messages.</returns>
        [HttpGet]
        [Route("lost")]
        [ProducesResponseType(typeof(IEnumerable<BLL.Contracts.Commands.Messages.GetFromId.MessageModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLost([FromQuery] GetLostMessagesFilter filter, CancellationToken cancellationToken)
        {
            if (filter is null)
                filter = new GetLostMessagesFilter();

            var result = await _mediator.Send(
                new GetMessagesFromIdRequest(filter.LastMessageId), cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Returns messages.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of messages.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedListModel<BLL.Contracts.Commands.Messages.GetList.MessageModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery]GetMessagesFilter filter, CancellationToken cancellationToken)
        {
            if (filter is null)
                filter = new GetMessagesFilter();

            var result = await _mediator.Send(
                new GetMessagesRequest(filter.LastMessageId, filter.PageSize, GetUserId()), cancellationToken);
            
            return Ok(result);
        }

        /// <summary>
        /// Deletes message by identifier.
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

            var response = await _mediator.Send(new DeleteMessageRequest(GetUserId(), id), cancellationToken);

            if(response.IsSuccess()) await _hubContext.Clients.All.Delete(id);

            var result = response.Match(
                success => NoContent(),
                blError => ProcessBLError(blError, _logger));

            return result;
        }

        /// <summary>
        /// Updates message.
        /// </summary>
        /// <param name="id">Message identifier.</param>
        /// <param name="model">Model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpPut]
        [Route("{id:long}")]
        [ProducesResponseType(typeof(BLError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update([FromRoute]long id, [FromBody] UpdateMessageModel model, CancellationToken cancellationToken)
        {
            if (WrongIdentifier(id))
                return BadRequest(ErrorMessages.WrongIdentifier);

            if (model is null)
                return BadRequest(ErrorMessages.RequiredBody);

            var response = await _mediator.Send(new UpdateMessageRequest(id, model.Text, GetUserId()), cancellationToken);

            if (response.IsSuccess()) 
                await _hubContext.Clients.All.Update(new UpdateMessageNotification(id, model.Text));

            var result = response.Match(
                success => NoContent(),
                blError => ProcessBLError(blError, _logger));

            return result;
        }

        /// <summary>
        /// Creates new message.
        /// </summary>
        /// <param name="model">Message model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpPost]
        [ProducesResponseType(typeof(BLError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody] CreateMessageModel model, CancellationToken cancellationToken)
        {
            if (model is null)
                return BadRequest(ErrorMessages.RequiredBody);

            var result = await CreateMessageAsync(GetUserId(), model.Text, false, null, cancellationToken);

            return result;
        }

        /// <summary>
        /// Creates reply.
        /// </summary>
        /// <param name="id">Message to reply identifier.</param>
        /// <param name="model">Model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpPost]
        [Route("{id:long}/reply")]
        [ProducesResponseType(typeof(BLError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Reply([FromRoute] long id, [FromBody] CreateReplyModel model, CancellationToken cancellationToken)
        {
            if (WrongIdentifier(id))
                return BadRequest(ErrorMessages.WrongIdentifier);

            if (model is null)
                return BadRequest(ErrorMessages.RequiredBody);

            var result = await CreateMessageAsync(GetUserId(), model.Text, true, id, cancellationToken);

            return result;
        }

        private async Task<IActionResult> CreateMessageAsync(
            long userId, 
            string text, 
            bool isReply, 
            long? messageToReplyId, 
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new CreateMessageRequest(userId, text, isReply, messageToReplyId), cancellationToken);

            if (response.IsSuccess())
                await _hubContext.Clients.All.Create(new CreateMessageNotification(response.AsT0.Value));

            var result = response.Match(
                createdMessage => NoContent(),
                blError => ProcessBLError(blError, _logger));

            return result;
        }
    }
}
