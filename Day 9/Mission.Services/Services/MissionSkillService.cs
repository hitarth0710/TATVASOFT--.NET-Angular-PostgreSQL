using Mission.Entities.Entities;
using Mission.Entities.Models.MissionSkill;
using Mission.Repositories.IRepositories;
using Mission.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Services.Services 
{
    public class MissionSkillService(IMissionSkillRepository missionSkillRepository) : IMissionSkillService
    {
        private readonly IMissionSkillRepository _missionSkillRepository = missionSkillRepository;

        /// <summary>
        /// get Mission Skill By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MissionSkillResponce> GetMissionSkillById(int id)
        {
            var missionSkill= await _missionSkillRepository.GetMissionSkillById(id);
            return new MissionSkillResponce
            {
                Id = missionSkill.Id,
                SkillName = missionSkill.SkillName,
                Status = missionSkill.Status
            };
        }

        /// <summary>
        /// Get All mission Skill
        /// </summary>
        /// <returns></returns>
        public async Task<IList<MissionSkillResponce>> GetAllMissionSkills()
        {
            var missionSkills = await _missionSkillRepository.GetAllMissionSkills();
            return missionSkills.Select(missionSkill => new MissionSkillResponce
            {
                Id = missionSkill.Id,
                SkillName = missionSkill.SkillName,
                Status = missionSkill.Status
            }).ToList();
        }


        /// <summary>
        /// Add Mission Skill
        /// </summary>
        /// <param name="missionSkillReq"></param>
        /// <returns></returns>
        public async Task AddMissionSkill(MissionSkillRequest missionSkillReq)
        {
            var missionSkill = new MissionSkill()
            {
                Id = missionSkillReq.Id,
                SkillName = missionSkillReq.SkillName,
                Status = missionSkillReq.Status
            };
            await _missionSkillRepository.AddMissionSkill(missionSkill);
        } 

        /// <summary>
        /// Update Mission Skill
        /// </summary>
        /// <param name="missionSkillReq"></param>
        /// <returns></returns>
        public async Task UpdateMissionSkill(MissionSkillRequest missionSkillReq)
        {
            var missionSkill = new MissionSkill()
            {
                Id = missionSkillReq.Id,
                SkillName = missionSkillReq.SkillName,
                Status = missionSkillReq.Status
            };
            await _missionSkillRepository.UpdateMissionSkill(missionSkill);
        }

        /// <summary>
        /// Delete Misssion Skill
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteMissionSkill(int id)
        {
            await _missionSkillRepository.DeleteMissionSkill(id);
        }
    }
}