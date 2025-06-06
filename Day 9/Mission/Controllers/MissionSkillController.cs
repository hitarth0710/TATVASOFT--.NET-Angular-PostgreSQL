using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models.MissionSkill;
using Mission.Services.IServices;

namespace Mission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionSkillController(IMissionSkillService missionSkillService) : ControllerBase
    {
        private readonly IMissionSkillService _missionSkillService = missionSkillService;

        [HttpGet]
        [Route("GetMissionSkillById")]
        public async Task<IActionResult> GetMissionSkillById([FromQuery] int id)
        {
            try
            {
                if (id <= 0) throw new Exception("Bad Request");
                var missionSkill = await _missionSkillService.GetMissionSkillById(id);
                return Ok(new ResponseResult() { Data = missionSkill, Result = ResponseStatus.Success, Message = "" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to get mission skill" });
            }
        }

        [HttpGet]
        [Route("GetAllMissionSkills")]
        public async Task<IActionResult> GetAllMissionSkills()
        {
            try
            {

                var missionSkills = await _missionSkillService.GetAllMissionSkills();
                return Ok(new ResponseResult() { Data = missionSkills, Result = ResponseStatus.Success, Message = "" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to get mission skills" });
            }
        }

        [HttpPost]
        [Route("AddMissionSkill")]
        public async Task<IActionResult> AddMissionSkill([FromBody] MissionSkillRequest missionSkillReq)
        {
            try
            {
                if (missionSkillReq == null) throw new Exception("Bad Request");
                await _missionSkillService.AddMissionSkill(missionSkillReq);
                return Ok(new ResponseResult() { Data = "null", Result = ResponseStatus.Success, Message = "Mission Skill Added Successfully" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to add mission skill" });
            }
        }


        [HttpPut]
        [Route("UpdateMissionSkill")]
        public async Task<IActionResult> UpdateMissionSkill([FromBody] MissionSkillRequest missionSkillReq)
        {
            try
            {
                if (missionSkillReq == null) throw new Exception("Bad Request");
                await _missionSkillService.UpdateMissionSkill(missionSkillReq);
                return Ok(new ResponseResult() { Data = "null", Result = ResponseStatus.Success, Message = "Mission Skill Updated Successfully" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to update mission skill" });
            }
        }


        [HttpDelete]
        [Route("DeleteMissionSkill")]
        public async Task<IActionResult> DeleteMissionSkill([FromQuery] int id)
        {
            try
            {
                if (id <= 0) throw new Exception("Bad Request");
                await _missionSkillService.DeleteMissionSkill(id);
                return Ok(new ResponseResult() { Data = "null", Result = ResponseStatus.Success, Message = "Mission Skill Deleted Successfully" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to delete mission skill" });
            }
        }

    }
}