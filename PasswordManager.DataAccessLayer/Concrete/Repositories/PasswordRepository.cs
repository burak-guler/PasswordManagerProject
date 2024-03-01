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
    public class PasswordRepository : GenericRepository<Password>, IPasswordRepository
    {
        public PasswordRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task Add(Password value)
        {
            var connection = await ConnectionDb();
            
            await connection.ExecuteAsync(PasswordQuery.ADD,value);
        }

        public async Task< Password> Get(int id)
        {
        
            var connection = await ConnectionDb();
            return await connection.QueryFirstAsync<Password>(PasswordQuery.GET,new {id});
        }

        public async Task< List<Password>> List()
        {
            
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<Password>(PasswordQuery.GET_LIST))?
                .ToList();
        }

        public async Task Remove(int id)
        {
            
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(PasswordQuery.REMOVE, new {id});
        }

        public async Task Update(Password value)
        {
            
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(PasswordQuery.UPDATE,value);
        }
    }
}
