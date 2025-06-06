using Mission.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionSkillRepository
    {
        Task<MissionSkill> GetMissionSkillById(int id);
        Task<IList<MissionSkill>> GetAllMissionSkills();
        Task AddMissionSkill(MissionSkill skill);
        Task UpdateMissionSkill(MissionSkill skill);
        Task DeleteMissionSkill(int id);
    }
}