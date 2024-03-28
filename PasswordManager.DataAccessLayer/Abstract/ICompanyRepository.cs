using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Abstract
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<int> Add(Company value, SqlConnection conn);
    }
}
