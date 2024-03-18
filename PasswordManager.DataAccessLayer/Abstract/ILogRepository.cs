using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Abstract
{
    public interface ILogRepository : IRepository<Log>
    {
        Task<List<Log>> GetAllByCompanyId(int companyId);
    }
}
