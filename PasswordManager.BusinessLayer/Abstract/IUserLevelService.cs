using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Abstract
{
    public interface IUserLevelService : IBaseService<UserLevel>
    {
        Task<List<UserLevel>> GetAllByCompanyId(int companyId);
    }
}
