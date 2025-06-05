using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services;
using Mission.Services.IServices;
using System;

namespace Mission.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminUserController : Controller
    {
        private readonly IAdminUserService _adminUserService;
        private readonly ILogger<AdminUserController> _logger;

        public AdminUserController(IAdminUserService adminUserService, ILogger<AdminUserController> logger)
        {
            _adminUserService = adminUserService;
            _logger = logger;
        }

        [HttpGet]
        [Route("UserDetailList")]
        public ActionResult UserDetailList()
        {
            try
            {
                var res = _adminUserService.UserDetailsList();
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "Users retrieved successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = $"Failed to get user list: {ex.Message}" });
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public ActionResult DeleteUser([FromQuery] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Invalid user ID" });
                }

                var res = _adminUserService.UserDelete(id);
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = $"Failed to delete user: {ex.Message}" });
            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        public ActionResult UpdateUser([FromBody] UpdateUserModel model)
        {
            try
            {
                _logger.LogInformation("Updating user with ID: {UserId}", model?.Id);
                
                if (model == null)
                {
                    return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "User data is required" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Invalid user data" });
                }

                var res = _adminUserService.UpdateUser(model);
                _logger.LogInformation("User updated successfully: {UserId}", model.Id);
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "User updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user: {UserId}, Error: {ErrorMessage}", model?.Id, ex.Message);
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = $"Failed to update user: {ex.Message}" });
            }
        }
        
        [HttpGet]
        [Route("GetUserById")]
        public ActionResult GetUserById([FromQuery] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Invalid user ID" });
                }

                var user = _adminUserService.GetUserById(id);
                if (user == null)
                {
                    return NotFound(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "User not found" });
                }

                return Ok(new ResponseResult() { Data = user, Result = ResponseStatus.Success, Message = "User retrieved successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = $"Error retrieving user: {ex.Message}" });
            }
        }
    }
}
