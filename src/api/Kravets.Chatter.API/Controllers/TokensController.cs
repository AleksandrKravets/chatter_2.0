using Kravets.Chatter.API.Models.Tokens;
using Kravets.Chatter.BLL.Contracts.Commands.Tokens.Refresh;
using Kravets.Chatter.BLL.Contracts.Models.Responses;
using Kravets.Chatter.Common.ResponseMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.API.Controllers
{
    /// <summary>
    /// Represents controller to manage tokens.
    /// </summary>
    [Route("api/tokens")]
    public class TokensController : ControllerBase
    {
        private readonly ILogger<TokensController> _logger;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="mediator">Mediator provider.</param>
        /// <param name="logger">Logging provider.</param>
        public TokensController(
            IMediator mediator,
            ILogger<TokensController> logger) : base(mediator) 
        {
            _logger = logger;
        }

        /// <summary>
        /// Refreshes tokens.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Tokens.</returns>
        [HttpPost]
        [Route("refresh")]
        [ProducesResponseType(typeof(RefreshTokensResultModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BLError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokensModel model, CancellationToken cancellationToken)
        {
            if (model is null)
                return BadRequest(ErrorMessages.RequiredBody);

            var response = await _mediator.Send(new RefreshTokensRequest(model.AccessToken, model.RefreshToken), cancellationToken);

            var result = response.Match(
                success => Ok(success.Value),
                blError => ProcessBLError(blError, _logger));

            return result;
        }
    }
}
