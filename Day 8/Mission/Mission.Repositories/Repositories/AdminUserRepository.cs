using Mission.Entities.Context;
using Mission.Entities.Models;
using Mission.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Repositories
{
    public class AdminUserRepository(MissionDbContext _missionDb) : IAdminUserRepository
    {
        public List<UserDetails> UserDetailsList()
        {
            var res = _missionDb.User.Where(x => !x.IsDeleted && x.UserType == "user").Select(x => new UserDetails(x));
            return res.ToList();
        }
        
        public string DeleteUser(int id)
        {
            var user = _missionDb.User.Where(x => x.Id == id).FirstOrDefault();

            if (user == null) throw new Exception("Account doesn't exist!");

            user.IsDeleted = true;
            user.ModifiedDate = DateTime.Now;
            _missionDb.User.Update(user);
            _missionDb.SaveChanges();
            return "Account deleted!";
        }
        
        public string UpdateUser(UpdateUserModel model)
        {
            try
            {
                var user = _missionDb.User.Where(x => x.Id == model.Id && !x.IsDeleted).FirstOrDefault();

                if (user == null) 
                    throw new KeyNotFoundException($"User with ID {model.Id} not found");

                // Check for email duplication
                var existingUser = _missionDb.User.FirstOrDefault(x => 
                    x.EmailAddress == model.EmailAddress && 
                    x.Id != model.Id && 
                    !x.IsDeleted);
                    
                if (existingUser != null)
                    throw new InvalidOperationException("Email address is already in use");

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.EmailAddress = model.EmailAddress;
                
                // Only update password if provided
                if (!string.IsNullOrEmpty(model.Password))
                {
                    user.Password = model.Password; // Consider adding password hashing
                }
                
                user.ModifiedDate = DateTime.Now;
                _missionDb.User.Update(user);
                _missionDb.SaveChanges();
                
                return "User updated successfully!";
            }
            catch (Exception)
            {
                throw; // Re-throw for controller to handle
            }
        }

        public UserDetails GetUserById(int id)
        {
            try
            {
                var user = _missionDb.User.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
                if (user == null)
                    return null;
                
                return new UserDetails(user);
            }
            catch (Exception ex)
            {
                // You could log the exception here if you have a logging framework
                throw; // Re-throw to be handled by the service layer
            }
        }
    }
}
