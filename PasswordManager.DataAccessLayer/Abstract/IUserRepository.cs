using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PasswordManager.DataAccessLayer.Abstract
{
    public interface IUserRepository : IRepository<UserViewModels>
    {
        Task<UserViewModels> Login(UserViewModels user);
        Task<UserViewModels> UserCheck(UserViewModels user);
        Task<UserViewModels> RoleCheck(int roleID, int UserID);
        Task AddUserToRole(int userID, int roleID);
        Task RemoveUserToRole(int userRoleID);
        Task<List<UserViewModels>> GetAllByCompanyId(int companyId);
        Task<List<UserRoleViewModels>> GetAllUserRoleByUserID(int userID);
    }
}
