using Mission.Entities;
using Mission.Entities.Models;
using Mission.Entities.Models.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Mission.Entities.Models.MissionListDto;
namespace Mission.Services.IServices
{
    public interface IMissionService
    {
        List<MissionListDto> GetMissionList();
        Task<bool> AddMission(MissionRequestViewModel model);
        List<DropDownResponseModel> GetMissionThemeList();
        Task<MissionRequestViewModel?> GetMissionById(int id);
        string DeleteMission(int id);
        string UpdateMission(AddMissionRequestModel model);
        List<DropDownResponseModel> GetMissionSkillList();
        
        Task<IList<MissionDetailResponseModel>> ClientSideMissionList(int userId);

        Task<bool> ApplyMission(AddMissionApplicationRequestModel model);
        List<MissionApplicationDto> GetMissionApplicationList();
        Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication);
    }
}
