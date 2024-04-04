using Dapper;
using Microsoft.Extensions.Configuration;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Repositories
{
    public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Task Add(Language value)
        {
            throw new NotImplementedException();
        }

        public Task<Language> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Language>> List()
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<Language>(LanguageQuery.GET_LIST))?
                .ToList();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Language value)
        {
            throw new NotImplementedException();
        }
    }
}
