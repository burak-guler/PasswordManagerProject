using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PasswordManager.DataAccessLayer.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Login(User user);
        Task<User> UserCheck(User user);
        Task<User> RoleCheck(int roleID, int UserID);
        Task AddUserToRole(int userID, int roleID);
        Task<List<User>> GetAllByCompanyId(int companyId);
    }
}
