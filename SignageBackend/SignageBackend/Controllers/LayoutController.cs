using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using System.Net;
using System.Net.NetworkInformation;

namespace SignageBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LayoutController : ControllerBase
    {
        private readonly ILayout _layout;      

        public LayoutController(ILayout layout)
        {
            _layout = layout;
        }

        [HttpPost("CreateOrUpdate")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult CreateOrUpdate([FromBody] LayoutView layoutView)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            int Layoutid = 0;
            if (layoutView != null)
            {
                Layoutid = _layout.CreateOrUpdate(layoutView);
            }

            return Ok(new ApiResponse { Id = Layoutid, Status = "success", Code = 200 });
        }

        [HttpGet("GetAll")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetAll(string? searchTerm = "", string sortBy = "Name",
            bool sortDescending = false, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _layout.GetAll(searchTerm, sortBy, sortDescending, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("GetById")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _layout.GetById(id);
            return Ok(result);
        }
    }
}
