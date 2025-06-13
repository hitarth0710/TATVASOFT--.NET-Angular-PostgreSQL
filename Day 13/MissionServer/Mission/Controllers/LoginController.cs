using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mission.Services;

namespace Mission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(ILoginService loginService, IWebHostEnvironment hostingEnvironment) : ControllerBase
    {
        private readonly ILoginService _loginService = loginService;
        private readonly IWebHostEnvironment _hostingEnvironment = hostingEnvironment;
        ResponseResult result = new ResponseResult();

        [HttpPost]
        [Route("LoginUser")]
        public ResponseResult LoginUser(LoginUserRequestModel model)
        {
            try
            {
                result.Data = _loginService.LoginUser(model);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserRequestModel registerUserRequest)
        {
            try
            {
                var res = await _loginService.RegisterUser(registerUserRequest);
                return Ok(new ResponseResult() { Data = "User registered", Result = ResponseStatus.Success, Message = "" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to add user." });
            }
        }
        [HttpGet("LoginUserDetailById/{id}")]
        public IActionResult LoginUserDetailById(int id)
        {
            var user = _loginService.GetUserDetailById(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(new ResponseResult() { Data = user, Result = ResponseStatus.Success, Message = "User updated successfully" });
        }
        [HttpPut("UpdateUser")]
        public ActionResult UpdateUser([FromForm] UserDetails u)
        {
            try
            {
                var res = _loginService.UserUpdate(u);
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "User updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = ex.Message });
            }
        }
    }
}
