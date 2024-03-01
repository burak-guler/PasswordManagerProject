using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public  CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CategoryAdd(Category category)
        {
           await _categoryRepository.Add(category);
        }

        public async Task CategoryRemove(int id)
        {
           await _categoryRepository.Remove(id);
        }

        public async Task CategoryUpdate(Category category)
        {
           await _categoryRepository.Update(category);
        }

        public async Task< Category> GetCategory(int id)
        {
           return await _categoryRepository.Get(id);
        }

        public async Task< List<Category>> GetCategoryList()
        {
            return await _categoryRepository.List();
        }
    }
}
