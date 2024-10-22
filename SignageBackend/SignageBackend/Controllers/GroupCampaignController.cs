﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using SignageBackend.DAL.Models;
using System.Net;

namespace SignageBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupCampaignController : ControllerBase
    {

        private readonly IGroupCampaign _groupCampaignView;
        private int ResID;

        public GroupCampaignController(IGroupCampaign groupCampaignView)
        {
            _groupCampaignView=groupCampaignView;
        }


        [HttpPost("CreateOrUpdate")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult CreateOrUpdate([FromBody] GroupCampaignView groupCampaignView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (groupCampaignView != null)
                {
                    ResID = _groupCampaignView.CreateOrUpdate(groupCampaignView);
                    if (ResID != 0)
                    {
                        if (groupCampaignView.GroupCampaignId == 0)
                        {
                            return Ok(new ApiResponse { Status = "Added Success", Code = 200 });
                        }
                        else
                        {
                            return Ok(new ApiResponse { Status = "Updated Success", Code = 200 });
                        }
                    }
                    else
                    {
                        return BadRequest(new ApiResponse { Status = "Error make changes", Code = 400 });
                    }
                }
                else
                {
                    return BadRequest(new ApiResponse { Status = "Error...", Code = 400 });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse { Status = ex.Message, Code = 500 });
            }
        }

        [HttpGet("GetById")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _groupCampaignView.GetById(id);
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
            var result = await _groupCampaignView.GetAll(searchTerm, sortBy, sortDescending, pageNumber, pageSize);
            if (result == null)
            {
                return Ok(new ApiResponse { Status = "No Record Found", Code = 200 });
            }
            return Ok(result);
        }

        [HttpGet("DeleteById")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _groupCampaignView.DeleteById(id);
            if (result == null)
            {
                return Ok(new ApiResponse { Status = "No Record Deleted", Code = 200 });
            }
            return Ok(result);
        }

    }
}
