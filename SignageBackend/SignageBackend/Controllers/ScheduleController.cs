using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using System.Net;

namespace SignageBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {

        private ApiResponse res;
        private readonly ISchedule _schedule;
        public ScheduleController(ISchedule schedule)
        {
            _schedule = schedule;
        }

        [HttpPost("CreateOrUpdate")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult CreateOrUpdate([FromBody] ScheduleView scheduleView)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (scheduleView != null)
            {
                res = _schedule.CreateOrUpdate(scheduleView);
            }

            return Ok(res);
        }

        [HttpGet("GetById")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _schedule.GetById(id);
            if (result == null)
            {
                return Ok(new ApiResponse { Status = "error", Code = 200, Message = "No record found." });
            }
            return Ok(result);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetAll(string? searchTerm = "", string sortBy = "Name",
            bool sortDescending = false, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _schedule.GetAll(searchTerm, sortBy, sortDescending, pageNumber, pageSize);
            if (result == null)
            {
                return Ok(new ApiResponse { Status = "error", Code = 200, Message = "No record found." });
            }
            return Ok(result);
        }

        [HttpGet("DeleteById")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _schedule.DeleteById(id);
            if (result == null)
            {
                return Ok(new ApiResponse { Status = "error", Code = 200, Message = "No record delete." });
            }
            return Ok(result);
        }
    }
}
