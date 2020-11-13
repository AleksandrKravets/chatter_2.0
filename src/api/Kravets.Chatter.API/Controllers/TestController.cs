using Kravets.Chatter.DAL.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.API.Controllers
{
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly StoredProcedureExecutor _spe;

        public TestController(IMediator mediator, StoredProcedureExecutor spe) : base(mediator) 
        {
            _spe = spe;
        }

        [HttpPost]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            // var response = await _spe.
            return Ok();
        }
    }
}
