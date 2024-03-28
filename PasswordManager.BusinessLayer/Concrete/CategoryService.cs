using Microsoft.Extensions.Configuration;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PasswordManager.BusinessLayer.Concrete
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private IConfiguration _configuration;

        public  CategoryService(ICategoryRepository categoryRepository, IConfiguration configuration)
        {
            _categoryRepository = categoryRepository;
            _configuration = configuration;
        }

        public async Task Add(Category entity, int? id)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var categoryID = await _categoryRepository.Add(entity, conn);

                        entity.CategoryID = categoryID;

                        await _categoryRepository.LangAdd(entity, conn);
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        throw new Exception(ex.Message);
                    }
                }
            }
            
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
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var categoryID = await _categoryRepository.Update(entity, conn);

                        entity.CategoryID = categoryID;

                        await _categoryRepository.LangUpdate(entity, conn);
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
    
}
