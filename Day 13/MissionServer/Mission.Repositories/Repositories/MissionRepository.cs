using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mission.Repositories.IRepositories;
using Mission.Entities;
using Mission.Entities.Context;
using Mission.Entities.Models;
using Mission.Entities.Models.CommonModels;
using System.IO;
using Microsoft.EntityFrameworkCore;
using static Mission.Entities.Models.MissionListDto;
using Mission.Entities.Models.CommonModels;

namespace Mission.Repositories.Repositories
{
    public class MissionRepository(MissionDbContext dbContext) : IMissionRepository
    {
        public List<MissionListDto> GetMissionList()
        {
            return dbContext.Missions
       .Where(x => !x.IsDeleted)
       .Select(x => new MissionListDto
       {
           Id = x.Id,
           MissionTitle = x.MissionTitle,
           MissionDescription = x.MissionDescription,
           MissionThemeName = x.MissionTheme.ThemeName,
           StartDate = x.StartDate,
           EndDate = x.EndDate
       })
       .ToList();
        }

        public async Task<bool> AddMission(Missions mission)
        {
            try
            {
                dbContext.Missions.Add(mission);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }

        public List<DropDownResponseModel> GetMissionThemeList()
        {
            var missionThemes = dbContext.MissionThemes
                .Where(mt => mt.Status == "active" && !mt.IsDeleted)
                .Select(mt => new DropDownResponseModel(mt.Id, mt.ThemeName))
                .Distinct()
                .ToList();

            return missionThemes;
        }

        public List<DropDownResponseModel> GetMissionSkillList()
        {
            var missionSkill = dbContext.MissionSkills
                .Where(ms => ms.Status == "active" && !ms.IsDeleted)
                .Select(ms => new DropDownResponseModel(ms.Id, ms.SkillName))
                .ToList();

            return missionSkill;
        }
        

        public async Task<MissionRequestViewModel?> GetMissionById(int id)
        {
            return await dbContext.Missions.Where(m => m.Id == id).Select(m => new MissionRequestViewModel()
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
        public string DeleteMission(int id)
        {
            dbContext.Missions
                .Where(ms => ms.Id == id)
                .ExecuteUpdate(ms => ms.SetProperty(property => property.IsDeleted, true));

            return "Mission Deleted Successfully..";
        }
        public string UpdateMission(AddMissionRequestModel model)
        {
            var missionToUpdate = dbContext.Missions.FirstOrDefault(ms => ms.Id == model.Id && !ms.IsDeleted) ?? throw new Exception("Mission not found.");

            bool missionAlreadyExists = dbContext.Missions
                .Any(ms => ms.Id != model.Id
                    && ms.MissionTitle.ToLower() == model.MissionTitle.ToLower()
                    && !ms.IsDeleted);

            if (missionAlreadyExists) throw new Exception("Mission Title Already Exist.");

            missionToUpdate.MissionTitle = model.MissionTitle;
            missionToUpdate.MissionDescription = model.MissionDescription;
            missionToUpdate.ModifiedDate = DateTime.UtcNow;
            missionToUpdate.TotalSheets = model.TotalSeats;
            missionToUpdate.StartDate = model.StartDate;
            missionToUpdate.EndDate = model.EndDate;
            missionToUpdate.MissionThemeId = model.MissionThemeId;
            missionToUpdate.MissionSkillId = model.MissionSkillId;
            missionToUpdate.CityId = model.CityId;
            missionToUpdate.CountryId = model.CountryId;
            missionToUpdate.MissionImages = model.MissionImages;

            dbContext.Missions.Update(missionToUpdate);
            dbContext.SaveChanges();

            return "Mission Updated Successfully..";
        }



        public async Task<IList<Missions>> ClientSideMissionList()
        {
            return await dbContext.Missions
                .Include(m => m.City)
                .Include(m => m.Country)
               .Include(m => m.MissionTheme)
               .Include(m => m.MissionApplications)
                .Where(m => !m.IsDeleted)
                .OrderBy(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task<bool> ApplyMission(AddMissionApplicationRequestModel model)
        {
            try
            {
                var mission = dbContext.Missions.Where(x => x.Id == model.MissionId).FirstOrDefault();

                if (mission == null) throw new Exception("Mission not found");

                var application = dbContext.MissionApplications.Where(x => x.MissionId == model.MissionId && x.UserId == model.UserId).FirstOrDefault();

                if (application != null) throw new Exception("Already applied!");

                MissionApplication app = new MissionApplication()
                {
                    UserId = model.UserId,
                    MissionId = model.MissionId,
                    AppliedDate = model.AppliedDate,
                    Seats = model.Sheet,
                    Status = model.Status,

                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                };

                mission.TotalSheets -= model.Sheet;

                await dbContext.MissionApplications.AddAsync(app);
                dbContext.Missions.Update(mission);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<MissionApplicationDto> GetMissionApplicationList()
        {
            //return dbContext.MissionApplications.Where(x => !x.IsDeleted).ToList();
            //    return dbContext.MissionApplications
            //.Where(x => !x.IsDeleted)
            //.Include(x => x.User)
            //.Include(x => x.Mission)
            //    .ThenInclude(m => m.MissionTheme) // assuming Mission has a navigation to MissionTheme

            return dbContext.MissionApplications
             .Where(x => !x.IsDeleted)
             .Include(x => x.User)
             .Include(x => x.Mission)
                 .ThenInclude(m => m.MissionTheme)
             .Select(x => new MissionApplicationDto
             {
                 Id = x.Id,
                 MissionId = x.MissionId,
                 UserId = x.UserId,
                 AppliedDate = x.AppliedDate,
                 Status = x.Status,
                 Seats = x.Seats,
                 MissionTitle = x.Mission.MissionTitle,
                 MissionTheme = x.Mission.MissionTheme.ThemeName,
                 FirstName = x.User.FirstName
             })
             .ToList();

        }

        public async Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication)
        {
            var tMissionApp = dbContext.MissionApplications.Where(x => x.Id == missionApplication.Id).FirstOrDefault();

            if (tMissionApp == null) throw new Exception("Mission application not found");

            tMissionApp.Status = true;
            tMissionApp.ModifiedDate = DateTime.Now;

            dbContext.MissionApplications.Update(tMissionApp);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
