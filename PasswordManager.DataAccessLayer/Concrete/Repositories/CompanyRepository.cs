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
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private IUserLevelRepository _userLevelRepository;   
        public CompanyRepository(IConfiguration configuration, IUserLevelRepository userLevelRepository) : base(configuration)
        {
            _userLevelRepository = userLevelRepository;
        }

        public async Task<int> Add(Company value, SqlConnection conn)
        {
            //var connection = await ConnectionDb();
            var companyId = await conn.QuerySingleAsync<int>(CompanyQuery.ADD, value);   
            return companyId;
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
