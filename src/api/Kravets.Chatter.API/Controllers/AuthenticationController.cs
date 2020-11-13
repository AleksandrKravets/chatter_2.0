using Kravets.Chatter.API.Models.Authentication;
using Kravets.Chatter.BLL.Contracts.Commands.Authentication.Authenticate;
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
    /// Represents controller to authenticate users.
    /// </summary>
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="mediator">Mediator provider.</param>
        /// <param name="logger">Logging provider.</param>
        public AuthenticationController(
            IMediator mediator,
            ILogger<AuthenticationController> logger) : base(mediator) 
        {
            _logger = logger;    
        }

        /// <summary>
        /// Authenticates user.
        /// </summary>
        /// <param name="model">User credentials.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The instance of <see cref="AuthenticationResultModel"></see>.</returns>
        [HttpPost]
        [Route("authenticate")]
        [ProducesResponseType(typeof(AuthenticationResultModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BLError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserModel model, CancellationToken cancellationToken)
        {
            if (model is null)
                return BadRequest(ErrorMessages.RequiredBody);

            var response = await _mediator.Send(new AuthenticationRequest(model.Nickname, model.Password), cancellationToken);

            var result = response.Match(
                success => Ok(success.Value),
                blError => ProcessBLError(blError, _logger));

            return result;
        }
    }
}
