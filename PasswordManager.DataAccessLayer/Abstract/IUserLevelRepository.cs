using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Abstract
{
    public interface IUserLevelRepository : IRepository<UserLevel>
    {
        Task<List<UserLevel>> GetAllByCompanyId(int companyId);
    }
}
