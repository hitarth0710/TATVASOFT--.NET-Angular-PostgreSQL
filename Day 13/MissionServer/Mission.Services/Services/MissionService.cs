using Mission.Entities;
using Mission.Entities.Models;
using Mission.Entities.Models.CommonModels;
using Mission.Repositories.IRepositories;
using Mission.Repositories.Repositories;
using Mission.Services.IServices;
using Mission.Entities.Models.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mission.Entities.Models.MissionListDto;

namespace Mission.Services.Services
{
    public class MissionService(IMissionRepository _missionRepository, IMissionSkillRepository _missionSkillRepository) : IMissionService
    {
        private readonly IMissionRepository missionRepository = _missionRepository;
        private readonly IMissionSkillRepository missionSkillRepository = _missionSkillRepository;
        public List<MissionListDto> GetMissionList()
        {
            return missionRepository.GetMissionList();
        }
        public Task<bool> AddMission(MissionRequestViewModel model)
        {
            var mission = new Entities.Missions()
            {
                CityId = model.CityId,
                CountryId = model.CountryId,
                EndDate = model.EndDate,
                MissionDescription = model.MissionDescription,
                MissionImages = model.MissionImages,
                MissionSkillId = model.MissionSkillId,
                MissionThemeId = model.MissionThemeId,
                MissionTitle = model.MissionTitle,
                StartDate = model.StartDate,
                TotalSheets = model.TotalSeats,
                MissionOrganisationDetail = "",
                MissionAvailability="",
                MissionDocuments="",
                MissionOrganisationName="",
                MissionVideoUrl="",
                MissionType="",
                IsDeleted=false,
                
            };


            return _missionRepository.AddMission(mission);
        }
        public List<DropDownResponseModel> GetMissionSkillList()
        {
            return missionRepository.GetMissionSkillList();
        }

        public List<DropDownResponseModel> GetMissionThemeList()
        {
            return missionRepository.GetMissionThemeList();
        }
        public Task<MissionRequestViewModel?> GetMissionById(int id)
        {
            return missionRepository.GetMissionById(id);
        }

        // int userId
        public async Task<IList<MissionDetailResponseModel>> ClientSideMissionList(int userId)
        {
            var missions = await _missionRepository.ClientSideMissionList();

            return missions.Select(m => new MissionDetailResponseModel()
            {
                Id = m.Id,
                EndDate = m.EndDate,
                StartDate = m.StartDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionTitle = m.MissionTitle,
                TotalSheets = m.TotalSheets,
                RegistrationDeadLine = m.RegistrationDeadLine,
                CityId = m.CityId,
                CityName = m.City.CityName,
                CountryId = m.CountryId,
                CountryName = m.Country.CountryName,
                MissionSkillId = m.MissionSkillId,
                MissionSkillName = _missionSkillRepository.GetMissionSkills(m.MissionSkillId),
                MissionThemeId = m.MissionThemeId,
                MissionThemeName = m.MissionTheme.ThemeName,

                MissionApplyStatus = m.MissionApplications.Any(m => m.UserId == userId) ? "Applied" : "Apply",
                MissionApproveStatus = m.MissionApplications.Any(m => m.UserId == userId && m.Status == true) ? "Approved" : "Applied",
                MissionStatus = m.RegistrationDeadLine < DateTime.Now.AddDays(-1) ? "Closed" : "Available"
            }).ToList();

        }

        public async Task<bool> ApplyMission(AddMissionApplicationRequestModel model)
        {
            return await _missionRepository.ApplyMission(model);
        }

        public List<MissionApplicationDto> GetMissionApplicationList()
        {
            return _missionRepository.GetMissionApplicationList();
        }

        public async Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication)
        {
            return await _missionRepository.MissionApplicationApprove(missionApplication);
        }
        public string DeleteMission(int id)
        {
            return missionRepository.DeleteMission(id);
        }
        public string UpdateMission(AddMissionRequestModel model)
        {
            return missionRepository.UpdateMission(model);
        }
      
    }
}
