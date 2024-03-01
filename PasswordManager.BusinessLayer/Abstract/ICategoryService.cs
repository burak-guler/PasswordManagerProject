using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        Task <List<Category>> GetCategoryList();
        Task CategoryAdd(Category category);
        Task<Category> GetCategory(int id);
        Task CategoryRemove(int id);
        Task CategoryUpdate(Category category);
    }
}
