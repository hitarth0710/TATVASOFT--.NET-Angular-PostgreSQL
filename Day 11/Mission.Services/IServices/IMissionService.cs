using Mission.Entities.Models;

namespace Mission.Services.IServices
{
    public interface IMissionService
    {
        Task<List<MissionRequestViewModel>> GetAllMissionAsync();
        Task<MissionRequestViewModel?> GetMissionById(int id);
        Task<bool> AddMission(MissionRequestViewModel model);
        Task<bool> UpdateMission(MissionRequestViewModel model);
        Task<bool> DeleteMission(int id);
        Task<(IList<MissionDetailResponseModel>, int totalCount)> ClientSideMissionList(int pageNumber = 1, int pageSize = 10);
    }
}
