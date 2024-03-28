using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Abstract
{
    public interface IUserLevelRepository : IRepository<UserLevel>
    {
        Task<List<UserLevel>> GetAllByCompanyId(int companyId);
        Task<int> CompanyLevel_Add(UserLevel value, SqlConnection conn);
        Task LangAdd(UserLevel value, SqlConnection conn);
        Task<int> Update(UserLevel value, SqlConnection conn);
        Task LangUpdate(UserLevel value, SqlConnection conn);
    }
}
