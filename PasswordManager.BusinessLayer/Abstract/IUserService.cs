using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PasswordManager.BusinessLayer.Abstract
{
    public interface IUserService : IBaseService<UserViewModels>
    {
        Task<UserViewModels> Login(UserViewModels user);
        Task AddUserToRole(int userID, int roleID);
        Task RemoveUserToRole(int userRoleID);
        Task<List<UserViewModels>> GetAllByCompanyId(int companyId);
        Task<List<UserRoleViewModels>> GetAllUserRoleByUserID(int userID);
    }
}
