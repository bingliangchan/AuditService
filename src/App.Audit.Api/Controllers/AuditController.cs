using App.Audit.Domain.Command;
using App.Audit.Domain.Model;
using App.Audit.Domain.Query;
using App.Common.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace App.Audit.Api.Controllers
{
    [Route("api/Audit")]
    public class AuditController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public AuditController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet(":{userId}")]
        public async Task<IActionResult> Get(int? userId, [FromQuery]DateTime? startDate, [FromQuery]DateTime? endDate)
        {
            //Assume time part is not important.
            var query = new GetAuditEventQuery { UserId = userId, StartDate = startDate, EndDate = endDate };
            var result = await _queryDispatcher.Request<GetAuditEventQuery, GetAuditEventQueryResult>(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuditEvent value)
        {
            await _commandDispatcher.Send(new AuditEventCreateCommand { Id = Guid.NewGuid(), Model = value });
            return Ok("Done");
        }
    }
}