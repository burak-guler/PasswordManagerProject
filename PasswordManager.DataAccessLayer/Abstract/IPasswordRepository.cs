using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Abstract
{
    public interface IPasswordRepository : IRepository<Password>
    {
        Task AddUserToPasswordAcces(int passwordID, int userID, int roleID);
        Task<List<Password>> GetAllByCompanyId(int companyId);
    }
}
