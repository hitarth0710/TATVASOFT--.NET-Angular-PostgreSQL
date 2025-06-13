
using Mission.Entities.Models;

namespace Mission.Services.IServices
{
    public interface IUserService
    {
        Task<UserDetails> GetUserById(int id);
        Task<string> DeleteUser(int id);
        Task<List<UserDetails>> GetAllUsers();
    }

}
