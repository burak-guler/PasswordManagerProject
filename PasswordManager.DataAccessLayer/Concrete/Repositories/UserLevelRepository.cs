using Dapper;
using Microsoft.Extensions.Configuration;
using PasswordManager.Core.Entity;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Repositories
{
    public class UserLevelRepository : GenericRepository<UserLevel> ,IUserLevelRepository
    {
        public UserLevelRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task Add(UserLevel value)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(UserLevelQuery.ADD, value);
        }

        public async Task<UserLevel> Get(int id)
        {
            var connection = await ConnectionDb();
            return await connection.QueryFirstAsync<UserLevel>(UserLevelQuery.GET, new { id });
        }

        public async Task<List<UserLevel>> GetAllByCompanyId(int companyId)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<UserLevel>(UserLevelQuery.GET_LIST_COMPANYID, new { companyId }))?
                .ToList();
        }

        public async Task<List<UserLevel>> List()
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<UserLevel>(UserLevelQuery.GET_LIST))?
                .ToList();
        }

        public async Task Remove(int id)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(UserLevelQuery.REMOVE, new { id });
        }

        public async Task Update(UserLevel value)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(UserLevelQuery.UPDATE, value);
        }
    }
}
