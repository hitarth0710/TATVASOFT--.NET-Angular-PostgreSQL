using Mission.Entities.Models;
using Mission.Repositories.Interface;
using Mission.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Services
{
    public class AdminUserService(IAdminUserRepository _adminUserRepository): IAdminUserService
    {
        public List<UserDetails> UserDetailsList()
        {
            return _adminUserRepository.UserDetailsList();
        }
        
        public string UserDelete(int id)
        {
            return _adminUserRepository.DeleteUser(id);
        }
        
        public string UpdateUser(UpdateUserModel model)
        {
            return _adminUserRepository.UpdateUser(model);
        }

        public UserDetails GetUserById(int id)
        {
            return _adminUserRepository.GetUserById(id);
        }
    }
}
