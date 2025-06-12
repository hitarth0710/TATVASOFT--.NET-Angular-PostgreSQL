using Mission.Entities;
using Mission.Entities.Models;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionRepository
    {
        Task<List<MissionRequestViewModel>> GetAllMissionAsync();
        Task<MissionRequestViewModel?> GetMissionById(int id);
        Task<bool> AddMission(Missions mission);
        Task<bool> UpdateMission(Missions mission);
        Task<bool> DeleteMission(int id);
        Task<IList<Missions>> ClientSideMissionList();
        Task<(IList<Missions>, int totalCount)> ClientSideMissionList(int pageNumber = 1, int pageSize = 10);
    }
}
