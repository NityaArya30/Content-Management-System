using System;
using Microsoft.AspNetCore.Mvc;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using System.Net;
using SignageBackend.DAL.Models;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SignageBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }


        [HttpPost("CreateOrUpdate")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult CreateOrUpdate([FromBody] UserView UserView)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (UserView != null)
                {
                    _user.CreateOrUpdate(UserView);
                }
                return Ok(new ApiResponse { Status = "User Added Success", Code = 200 });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse { Status = ex.Message, Code = 200 });
            }
        }

        [HttpGet("GetById")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _user.GetById(id);
            if (result == null)
            {
                return Ok(new ApiResponse { Status = "No Record Found", Code = 200 });
            }
            return Ok(result);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetAll(string? searchTerm = "", string sortBy = "Name",
            bool sortDescending = false, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _user.GetAll(searchTerm, sortBy, sortDescending, pageNumber, pageSize);
            if (result == null)
            {
                return Ok(new ApiResponse { Status = "No Record Found", Code = 200 });
            }
            return Ok(result);
        }
        [HttpGet("GetAllEmail")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var result = await _user.GetByEmail(email);
            if (result == null)
            {
                return Ok(new ApiResponse { Status = "No Record Found", Code = 200 });
            }
            return Ok(result);
        }

        [HttpGet("DeleteById")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _user.DeleteById(id);
            if (result == null)
            {
                return Ok(new ApiResponse { Status = "No Record Deleted", Code = 200 });
            }
            return Ok(result);
        }
    }
}
