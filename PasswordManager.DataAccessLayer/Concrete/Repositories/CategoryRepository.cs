using Dapper;
using Microsoft.Extensions.Configuration;
using PasswordManager.Core.Entity;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        public async Task<int> Add(Category value, SqlConnection conn)
        {
            //var connection = await ConnectionDb();
            return await conn.QuerySingleAsync<int>(CategoryQuery.ADD, value);
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

        public async Task LangAdd(Category value, SqlConnection conn)
        {
            //var connection = await ConnectionDb();
            await conn.ExecuteAsync(CategoryQuery.LANG_ADD,value);
        }

        public async Task LangUpdate(Category value, SqlConnection conn)
        {
            //var connection = await ConnectionDb();
            await conn.ExecuteAsync(CategoryQuery.LANG_UPDATE, new { CategoryName = value.CategoryName, CategoryID = value.CategoryID, LangID=value.LangID });
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

        public async Task<int> Update(Category value, SqlConnection conn)
        {
            //var connection = await ConnectionDb();
            return await conn.QuerySingleAsync<int>(CategoryQuery.UPDATE, value);
        }
    }
}
