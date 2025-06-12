using Microsoft.EntityFrameworkCore;
using Mission.Entities;
using Mission.Entities.Context;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;

namespace Mission.Repositories.Repositories
{
    public class MissionRepository(MissionDbContext dbContext) : IMissionRepository
    {
        private readonly MissionDbContext _dbContext = dbContext;

        public Task<List<MissionRequestViewModel>> GetAllMissionAsync()
        {
            return _dbContext.Missions.Select(m => new MissionRequestViewModel()
            {
                Id = m.Id,
                CityId = m.CityId,
                CountryId = m.CountryId,
                EndDate = m.EndDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionSkillId = m.MissionSkillId,
                MissionThemeId = m.MissionThemeId,
                MissionTitle = m.MissionTitle,
                StartDate = m.StartDate,
                TotalSeats = m.TotalSheets ?? 0,
            }).ToListAsync();
        }

        public async Task<MissionRequestViewModel?> GetMissionById(int id)
        {
            return await _dbContext.Missions.Where(m => m.Id == id).Select(m => new MissionRequestViewModel()
            {
                Id = m.Id,
                CityId = m.CityId,
                CountryId = m.CountryId,
                EndDate = m.EndDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionSkillId = m.MissionSkillId,
                MissionThemeId = m.MissionThemeId,
                MissionTitle = m.MissionTitle,
                StartDate = m.StartDate,
                TotalSeats = m.TotalSheets ?? 0,
            }).FirstOrDefaultAsync();
        }

        public async Task<bool> AddMission(Missions mission)
        {
            try
            {
                _dbContext.Missions.Add(mission);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log exception details
                // logger.LogError(ex, "Error adding mission");
                throw new Exception($"Failed to add mission: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateMission(Missions mission)
        {
            try
            {
                var existingMission = await _dbContext.Missions.FindAsync(mission.Id);
                
                if (existingMission == null)
                    return false;
                    
                existingMission.MissionTitle = mission.MissionTitle;
                existingMission.MissionDescription = mission.MissionDescription;
                existingMission.CountryId = mission.CountryId;
                existingMission.CityId = mission.CityId;
                existingMission.MissionThemeId = mission.MissionThemeId;
                existingMission.MissionSkillId = mission.MissionSkillId;
                existingMission.StartDate = mission.StartDate;
                existingMission.EndDate = mission.EndDate;
                existingMission.MissionImages = mission.MissionImages;
                existingMission.TotalSheets = mission.TotalSheets;
                existingMission.RegistrationDeadLine = mission.RegistrationDeadLine;
                existingMission.ModifiedDate = DateTime.UtcNow;
                
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"Failed to update mission: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteMission(int id)
        {
            try
            {
                var mission = await _dbContext.Missions.FindAsync(id);
                if (mission == null)
                    return false;
                    
                mission.IsDeleted = true;
                mission.ModifiedDate = DateTime.UtcNow;
                
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"Failed to delete mission: {ex.Message}", ex);
            }
        }

        public async Task<IList<Missions>> ClientSideMissionList()
        {
            return await _dbContext.Missions
                .Include(m => m.City)
                .Include(m => m.Country)
                .Include(m => m.MissionTheme)
                .Where(m => !m.IsDeleted)
                .OrderBy(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task<(IList<Missions>, int totalCount)> ClientSideMissionList(int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.Missions
                .Include(m => m.City)
                .Include(m => m.Country)
                .Include(m => m.MissionTheme)
                .Where(m => !m.IsDeleted)
                .OrderBy(m => m.CreatedDate);

            var totalCount = await query.CountAsync();

            var missions = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (missions, totalCount);
        }
    }
}
