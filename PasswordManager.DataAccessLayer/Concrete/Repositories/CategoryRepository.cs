using Dapper;
using Microsoft.Extensions.Configuration;
using PasswordManager.Core.Entity;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Query;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PasswordManager.DataAccessLayer.Concrete.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task Add(Category value)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(CategoryQuery.ADD, value);
        }

        public async Task<Category> Get(int id)
        {
            var connection = await ConnectionDb();
            return await connection.QueryFirstOrDefaultAsync<Category>(CategoryQuery.GET, new { id });
        }

        public async Task<List<Category>> GetAllByCompanyId(int companyId)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<Category>(CategoryQuery.GET_LIST_COMPANYID, new { companyId }))?
                .ToList();
        }

        public async Task<List<Category>> List()
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<Category>(CategoryQuery.GET_LIST))?
                .ToList();
        }

        public async Task Remove(int id)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(CategoryQuery.REMOVE, new { id });
        }

        public async Task Update(Category value)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(CategoryQuery.UPDATE, value);
        }
    }
}
