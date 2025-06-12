using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mission.Entities.Context;
using Mission.Entities.Entities;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;

namespace Mission.Repositories.Repositories
{
    public class MissionThemeRepository : IMissionThemeRepository
    {
        private readonly MissionDbContext _missionDbContext;

        public MissionThemeRepository(MissionDbContext missionDbContext)
        {
            _missionDbContext = missionDbContext;
        }

        public async Task<bool> AddMissionTheme(MissionTheme missionTheme)
        {
            _missionDbContext.MissionThemes.Add(missionTheme);
            await _missionDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMissionTheme(int missionThemeId)
        {
            var missionThemeExistingInDb = await _missionDbContext.MissionThemes.FindAsync(missionThemeId);

            if (missionThemeExistingInDb == null)
                return false;

            _missionDbContext.Remove(missionThemeExistingInDb);
            await _missionDbContext.SaveChangesAsync();
            return true;
        }

        public Task<List<MissionThemeViewModel>> GetAllMissionTheme(bool? activeOnly = null)
        {
            var query = _missionDbContext.MissionThemes.AsQueryable();
            
            if (activeOnly.HasValue && activeOnly.Value)
            {
                query = query.Where(m => m.Status == "Active");
            }
            
            return query.Select(m => new MissionThemeViewModel()
                {
                    Id = m.Id,
                    Status = m.Status,
                    ThemeName = m.ThemeName,
                })
                .ToListAsync();
        }

        public Task<MissionThemeViewModel?> GetMissionThemeById(int missionThemeId)
        {
            return _missionDbContext.MissionThemes
                .Where(m => m.Id == missionThemeId)
                .Select(m => new MissionThemeViewModel()
                {
                    Id = m.Id,
                    Status = m.Status,
                    ThemeName = m.ThemeName,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateMissionTheme(MissionTheme missionTheme)
        {
            var missionThemeExistingInDb = await _missionDbContext.MissionThemes.FindAsync(missionTheme.Id);

            if (missionThemeExistingInDb == null)
                return false;

            missionThemeExistingInDb.ThemeName = missionTheme.ThemeName;
            missionThemeExistingInDb.Status = missionTheme.Status;
            await _missionDbContext.SaveChangesAsync();

            return true;
        }
    }
}
