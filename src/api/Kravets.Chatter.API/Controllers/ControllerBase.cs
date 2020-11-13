using Kravets.Chatter.API.Extensions;
using Kravets.Chatter.API.Models.Errors;
using Kravets.Chatter.BLL.Contracts.Models.Responses;
using Kravets.Chatter.Common.Constants;
using Kravets.Chatter.Common.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Kravets.Chatter.API.Controllers
{
    /// <summary>
    /// Represents controller with common functional.
    /// </summary>
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public class ControllerBase : Controller
    {
        protected readonly IMediator _mediator;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public ControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected long GetUserId() => User.GetLongClaim(CustomClaims.UserId);

        protected bool WrongIdentifier(long id) => id < 1;

        protected IActionResult ProcessBLError(BLError error, ILogger logger)
        {
            EventLog.BLError(logger, error.Message);
            return BadRequest(error);
        }
    }
}
