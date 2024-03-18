using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Abstract
{
    public interface ICategoryService : IBaseService<Category>
    {
        Task<List<Category>> GetAllByCompanyId(int companyId);
    }
}
