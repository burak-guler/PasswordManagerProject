using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Abstract
{
    public interface IPasswordService : IBaseService<Password>
    {
        Task AddUserToPasswordAcces(int passwordID, int userID, int roleID);
        Task<List<Password>> GetAllByCompanyId(int companyId);
    }
}
