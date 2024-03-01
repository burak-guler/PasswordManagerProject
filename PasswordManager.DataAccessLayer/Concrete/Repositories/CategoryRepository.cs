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
            //var query = "INSERT INTO dbo.Tbl_Category VALUES (@CategoryName)";

             var connection = await ConnectionDb();            

            await connection.ExecuteAsync(CategoryQuery.ADD, value);           
        }

        public async Task <Category> Get(int id)
        {
            var query = "SELECT * FROM dbo.Tbl_Category WHERE CategoryID=@id";

            var connection = await ConnectionDb();        
            
            return await connection.QueryFirstOrDefaultAsync<Category>(query, new {id});
        }

        public async Task< List<Category>> List()
        {
            var query = "SELECT * FROM dbo.Tbl_Category";

            var connection = await ConnectionDb();

            return (await connection.QueryAsync<Category>(query))?
                .ToList();
        }

        public async Task Remove(int id)
        {
            var query = "DELETE FROM dbo.Tbl_Category WHERE CategoryID = @id";
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(query, new {id});
        }

        public async Task Update(Category value)
        {
            var query = "UPDATE dbo.Tbl_Category SET CategoryName = @CategoryName WHERE CategoryID =@CategoryID";
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(query,value);            
        }
    }
}
