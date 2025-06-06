using Microsoft.EntityFrameworkCore;
using Mission.Entities.Context;
using Mission.Entities.Entities;
using Mission.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission.Repositories.Repositories
{
    public class MissionSkillRepository : IMissionSkillRepository
    {
        private readonly MissionDbContext _missionDBContext;

        public MissionSkillRepository(MissionDbContext cIDbcontext)
        {
            _missionDBContext = cIDbcontext;
        }

        // Get by Id
        public async Task<MissionSkill> GetMissionSkillById(int id)
        {
            var missionSkill = await _missionDBContext.MissionSkills
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);

            if (missionSkill == null)
            {
                throw new KeyNotFoundException($"MissionSkill with ID {id} not found.");
            }

            return missionSkill;
        }

        // Get all
        public async Task<IList<MissionSkill>> GetAllMissionSkills()
        {
            return await _missionDBContext.MissionSkills
                .Where(m => !m.IsDeleted)
                .ToListAsync();
        }

        // Add MissionSkill
        public async Task AddMissionSkill(MissionSkill missionSkill)
        {
            var isExist = await _missionDBContext.MissionSkills
                .AnyAsync(m => m.SkillName.ToLower() == missionSkill.SkillName.ToLower() && !m.IsDeleted);

            if (isExist)
                throw new ArgumentException("MissionSkill already exists.");

            var newSkill = new MissionSkill
            {
                SkillName = missionSkill.SkillName,
                Status = missionSkill.Status,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            _missionDBContext.MissionSkills.Add(newSkill);
            await _missionDBContext.SaveChangesAsync();
        }

        // Update MissionSkill
        public async Task UpdateMissionSkill(MissionSkill missionSkillReq)
        {
            var missionSkill = await _missionDBContext.MissionSkills
                .FirstOrDefaultAsync(m => m.Id == missionSkillReq.Id && !m.IsDeleted);

            if (missionSkill == null)
                throw new Exception("Skill doesn't exist");

            var isExist = await _missionDBContext.MissionSkills
                .AnyAsync(m => m.SkillName.ToLower() == missionSkillReq.SkillName.ToLower()
                            && m.Id != missionSkillReq.Id && !m.IsDeleted);

            if (isExist)
                throw new Exception("MissionSkill already exists.");

            missionSkill.SkillName = missionSkillReq.SkillName;
            missionSkill.Status = missionSkillReq.Status;
            missionSkill.ModifiedDate = DateTime.UtcNow;

            await _missionDBContext.SaveChangesAsync();
        }

        // Delete MissionSkill
        public async Task DeleteMissionSkill(int id)
        {
            var missionSkill = await _missionDBContext.MissionSkills
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);

            if (missionSkill == null)
                throw new Exception("MissionSkill not found");

            missionSkill.IsDeleted = true;
            missionSkill.ModifiedDate = DateTime.UtcNow;

            _missionDBContext.MissionSkills.Update(missionSkill);
            await _missionDBContext.SaveChangesAsync();
        }
    }
}