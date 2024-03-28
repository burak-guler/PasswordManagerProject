using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetAllByCompanyId(int companyId);
        Task<int> Add(Category value,SqlConnection conn);
        Task<int> Update(Category value, SqlConnection conn);
        Task LangAdd(Category value, SqlConnection conn);
        Task LangUpdate(Category value, SqlConnection conn);
    }
}
