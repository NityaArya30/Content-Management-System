using System;
using Microsoft.AspNetCore.Mvc;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using System.Net;
using SignageBackend.DAL.Models;

namespace SignageBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
	{
		private readonly IRole _role;
		public RoleController(IRole role)
		{
			_role = role;
		}

        [HttpPost("CreateOrUpdate")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult CreateOrUpdate([FromBody] RoleView roleView)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (roleView != null)
            {
                _role.CreateOrUpdate(roleView);
            }

            return Ok(new ApiResponse { Status = "success", Code = 200 });
        }

        [HttpGet("GetAll")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _role.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _role.GetById(id);
            return Ok(result);
        }


    }
}

