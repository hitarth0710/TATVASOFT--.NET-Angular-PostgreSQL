using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services.IServices;
using Mission.Entities.Models.CommonModels;
using Mission.Services.Services;
using Microsoft.AspNetCore.Authorization;
namespace Mission.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissionController(IMissionService missionService) : Controller
    {
        [HttpGet]
        [Route("MissionList")]
        public ResponseResult MissionList()
        {
            return new ResponseResult() { Data = missionService.GetMissionList(), Message = "", Result = ResponseStatus.Success };
        }

        [HttpPost]
        [Route("AddMission")]
        public async Task<IActionResult> AddMission(MissionRequestViewModel model)
        {
            var response = await missionService.AddMission(model); 
            return Ok(new ResponseResult()
            {
                Data = response,
                Result = ResponseStatus.Success,
                Message = ""
            });
        }


        [HttpGet]
        [Route("GetMissionThemeList")]
        public ResponseResult MissionThemeList()
        {
            ResponseResult result = new ResponseResult();
            try
            {
                result.Data = missionService.GetMissionThemeList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("GetMissionSkillList")]
        public ResponseResult MissionSkillList()
        {
            ResponseResult result = new ResponseResult();
            try
            {
                result.Data = missionService.GetMissionSkillList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }


        [HttpGet]
        [Route("MissionDetailById/{id:int}")]
        public async Task<IActionResult> GetMissionById(int id)
        {
            var response = await missionService.GetMissionById(id);
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpDelete]
        [Route("DeleteMission/{id}")]
      
        public ResponseResult DeleteMission(int id)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                result.Data = missionService.DeleteMission(id);
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
        [Route("UpdateMission")]
        [Authorize]
        public ResponseResult UpdateMission(AddMissionRequestModel  model)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                result.Data = missionService.UpdateMission(model);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("MissionApplicationList")]
        public IActionResult MissionApplicationList()
        {
            var response = missionService.GetMissionApplicationList();
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpPost]
        [Route("MissionApplicationApprove")]
        public async Task<IActionResult> MissionApplicationApprove(UpdateMissionApplicationModel missionApp)
        {
            try
            {
                var ret = await missionService.MissionApplicationApprove(missionApp);
                return Ok(new ResponseResult() { Data = ret, Message = string.Empty, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Message = ex.Message, Result = ResponseStatus.Error });
            }
        }
    }
}
