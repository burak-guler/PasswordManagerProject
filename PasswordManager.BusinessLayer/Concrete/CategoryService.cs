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

        public async Task Add(Category entity, int? id)
        {
            await _categoryRepository.Add(entity);
        }    
        public async Task<List<Category>> GetAll()
        {
            return await _categoryRepository.List();
        }

        public async Task<List<Category>> GetAllByCompanyId(int companyId)
        {
            return await _categoryRepository.GetAllByCompanyId(companyId);
        }

        public async Task<Category> GetById(int id)
        {
            return await _categoryRepository.Get(id);
        }      
       
        public async Task Remove(int id)
        {
            await _categoryRepository.Remove(id);
        }

        public async Task Update(Category entity)
        {
            await _categoryRepository.Update(entity);
        }
    }
}
