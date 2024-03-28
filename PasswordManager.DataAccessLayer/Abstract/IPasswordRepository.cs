using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Abstract
{
    public interface IPasswordRepository : IRepository<PasswordViewModels>
    {
        Task AddUserToPasswordAcces(int passwordID, int userID, int roleID);
        Task RemoveUserToPasswordAcces(int passwordID, int userID, int roleID);
        Task<List<PasswordViewModels>> GetAllByCompanyId(int companyId);
        Task<List<PasswordViewModels>> GetAllByUserId(int userID);
        Task<PasswordViewModels> PASSWORDROLE_CHECK(int passwordID, int userID, int roleID);



    }
}
