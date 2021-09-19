using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volue.Application.Common.Models;
using Volue.Application.DataPoints;
using Volue.Application.DataPoints.Commands;
using Volue.Application.DataPoints.Queries;

namespace Volue.WebUI.Controllers
{
    // [Authorize]
    [Route("api")]
    public class DataPointsController : ApiControllerBase 
    {
        [HttpGet("all")]
        public async Task<ActionResult<PaginatedList<DataPointDto>>> All([FromQuery] GetDataPointsWithPaginationQuery withPaginationQuery) 
        {
            return await Mediator.Send(withPaginationQuery);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<AvgSumDto>> CalculateAvgSum(string name, [FromQuery] int? from, [FromQuery] int? to)
        {
            return await Mediator.Send(new CalculateAvgSumCommand(name, from, to));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] List<DataPointDto> dataPoints) 
        { 
            await Mediator.Send(new CreateDataPointsCommand(dataPoints));
            return Accepted();
        }
    }
}