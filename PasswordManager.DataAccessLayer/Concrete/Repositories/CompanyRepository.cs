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
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task Add(Company value)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(CompanyQuery.ADD, value);
        }

        public async Task<Company> Get(int id)
        {
            var connection = await ConnectionDb();
            return await connection.QueryFirstOrDefaultAsync<Company>(CompanyQuery.GET, new { id });
        }

        public async Task<List<Company>> List()
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<Company>(CompanyQuery.GET_LIST))?
                .ToList();
        }

        public async Task Remove(int id)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(CompanyQuery.REMOVE, new { id });
        }

        public async Task Update(Company value)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(CompanyQuery.UPDATE, value);
        }
    }
}
