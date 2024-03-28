using Dapper;
using Microsoft.Extensions.Configuration;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
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
    public class PasswordRepository : GenericRepository<PasswordViewModels>, IPasswordRepository
    {
        public PasswordRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task Add(PasswordViewModels value)
        {
            var connection = await ConnectionDb();            
            await connection.ExecuteAsync(PasswordQuery.ADD,value);
        }

        public async Task<PasswordViewModels> Get(int id)
        {        
            var connection = await ConnectionDb();
            return await connection.QueryFirstAsync<PasswordViewModels>(PasswordQuery.GET,new { PasswordID = id });
        }

        public async Task< List<PasswordViewModels>> List()
        {            
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<PasswordViewModels>(PasswordQuery.GET_LIST))?
                .ToList();
        }

        public async Task Remove(int id)
        {            
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(PasswordQuery.REMOVE, new {id});
        }

        public async Task Update(PasswordViewModels value)
        {            
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(PasswordQuery.UPDATE,value);
        }

        public async Task AddUserToPasswordAcces(int passwordID , int userID , int roleID)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(PasswordQuery.PasswordAccesADD, new {PasswordID = passwordID, UserID = userID, RoleID = roleID});
        }

        public async Task<List<PasswordViewModels>> GetAllByCompanyId(int companyId)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<PasswordViewModels>(PasswordQuery.GET_LIST_COMPANYID, new { companyId }))?
                .ToList();
        }

        public async Task<PasswordViewModels> PASSWORDROLE_CHECK(int passwordID, int userID, int roleID)
        {
            var connection = await ConnectionDb();
            return await connection.QueryFirstAsync<PasswordViewModels>(PasswordQuery.PASSWORDROLE_CHECK, new { PasswordID= passwordID , UserID=userID , RoleID=roleID });
        }

        public async Task RemoveUserToPasswordAcces(int passwordID, int userID, int roleID)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(PasswordQuery.PasswordAccesRemove, new { PasswordID = passwordID, UserID = userID, RoleID = roleID });
        }

        public async Task<List<PasswordViewModels>> GetAllByUserId(int userID)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<PasswordViewModels>(PasswordQuery.GET_LIST_USERID, new { userID }))?
                .ToList();
        }
    }
}
