using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.Services;
using SignageBackend.BAL.ViewModels;
using System.Net;
using SignageBackend.Common.Utilites;

namespace SignageBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUser _user;

        public LoginController(IUser user)
        {
            _user = user;
        }

        [HttpPost("Authentication")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult Authentication([FromBody] UserLoginView userLoginView)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (userLoginView != null)
            {
                if (userLoginView.Email != null && userLoginView.Password != null)
                {
                    AuthenticateView authenticateView = _user.Authenticate(userLoginView);

                    return Ok(new AuthenticateView { Id = authenticateView.Id, FirstName = authenticateView.FirstName, LastName = authenticateView.LastName, Email = authenticateView.Email,  UserName = authenticateView.UserName });
                }
                else
                {
                    return Ok(new ApiResponse { Status = "Bad Credentials", Code = 401 });
                }

            }
            else
            {
                return Ok(new ApiResponse { Status = "Bad Credentials", Code = 401 });
            }

            // return Ok(new ApiResponse { Status = "success", Code = 200 });
        }
    }
}


