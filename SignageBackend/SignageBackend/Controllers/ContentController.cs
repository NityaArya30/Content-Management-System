using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SignageBackend.DAL.Models;
using System.Collections.Generic;


namespace SignageBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {

        private readonly IContent _content;
        public ContentController(IContent content)
        {
            _content = content;
        }
        [HttpPost("CreateOrUpdate")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        //public async Task<IActionResult> UploadImage1( IFormFile FormFileimg)
        public IActionResult CreateOrUpdate([FromForm] ContentView contentView)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (contentView != null)
            {
                _content.CreateOrUpdate(contentView);
            }

            return Ok(new ApiResponse { Status = "success", Code = 200 });
        }

        [HttpGet("GetById")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _content.GetById(id);
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
            var result = await _content.GetAll(searchTerm, sortBy, sortDescending, pageNumber, pageSize);
            return Ok(result); 
        }

        [HttpGet("DeleteById")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _content.DeleteById(id);
            return Ok();
        }
        
        /*
        [HttpPost("UploadImage1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> UploadImage1( IFormFile FormFileimg)
        {
            await _content.UploadImage1(FormFileimg);
            return Ok();
        }*/
        
    }
}