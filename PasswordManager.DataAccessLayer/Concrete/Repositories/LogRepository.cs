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
    public class LogRepository :GenericRepository<Log> , ILogRepository
    {
        public LogRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task Add(Log value)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(LogQuery.ADD, value);
        }

        public async Task<Log> Get(int id)
        {
            var connection = await ConnectionDb();
            return await connection.QueryFirstOrDefaultAsync<Log>(LogQuery.GET, new { id });
        }

        public async Task<List<Log>> GetAllByCompanyId(int companyId)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<Log>(LogQuery.GET_LIST_COMPANYID, new { companyId }))?
                .ToList();
        }

        public async Task<List<Log>> List()
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<Log>(LogQuery.GET_LIST))?
                .ToList();
        }

        public async Task Remove(int id)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(LogQuery.REMOVE, new { id });
        }

        public async Task Update(Log value)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(LogQuery.UPDATE, value);
        }
    }
}
