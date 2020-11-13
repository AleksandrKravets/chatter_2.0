using Kravets.Chatter.API.Models.Accounts;
using Kravets.Chatter.BLL.Contracts.Commands.Accounts.Create;
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
    /// Represents controller to manage users accounts.
    /// </summary>
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="mediator">Mediator provider.</param>
        /// <param name="logger">Logging provider.</param>
        public AccountController(
            IMediator mediator,
            ILogger<AccountController> logger) : base(mediator) 
        {
            _logger = logger;    
        }
    
        /// <summary>
        /// Creates user account.
        /// </summary>
        /// <param name="model">User credentials.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpPost]
        [ProducesResponseType(typeof(BLError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]RegisterAccountModel model, CancellationToken cancellationToken)
        {
            if (model is null)
                return BadRequest(ErrorMessages.RequiredBody);

            var response = await _mediator.Send(new CreateAccountRequest(model.Nickname, model.Password), cancellationToken);

            var result = response.Match(
                success => NoContent(),
                blError => ProcessBLError(blError, _logger));

            return result;
        }
    }
}
