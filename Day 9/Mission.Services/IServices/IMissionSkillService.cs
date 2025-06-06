using Mission.Entities.Models.MissionSkill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Services.IServices
{
    public interface IMissionSkillService
    {
        Task<MissionSkillResponce> GetMissionSkillById(int id);
        Task<IList<MissionSkillResponce>> GetAllMissionSkills();
        Task AddMissionSkill(MissionSkillRequest missionSkillReq);
        Task UpdateMissionSkill(MissionSkillRequest missionSkillReq);
        Task DeleteMissionSkill(int id);

    }
}