using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Abstract
{
    public interface IPasswordService : IBaseService<PasswordViewModels>
    {
        Task AddUserToPasswordAcces(int passwordID, int userID, int roleID);
        Task RemoveUserToPasswordAcces(int passwordID, int userID, int roleID);
        Task<List<PasswordViewModels>> GetAllByCompanyId(int companyId);
        Task<List<PasswordViewModels>> GetAllByUserId(int userID);
        Task<PasswordViewModels> GetById(int id, int currentID);
        Task Remove(int id, int currentID);
        Task Update(PasswordViewModels entity, int currentID);
        Task<List<PasswordViewModels>> PasswordAccesGetList(int userID, int roleID);
    }
}
