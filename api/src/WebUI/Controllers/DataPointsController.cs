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
    public class DataPointsController : ApiControllerBase 
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<DataPointDto>>> GetTodoItemsWithPagination([FromQuery] GetDataPointsWithPaginationQuery withPaginationQuery) 
        {
            return await Mediator.Send(withPaginationQuery);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateDataPoints([FromBody] List<DataPointDto> dataPoints) 
        { 
            await Mediator.Send(new CreateDataPointsCommand(dataPoints));
            return Accepted();
        }
    }
}