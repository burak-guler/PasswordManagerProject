
using Dapper;
using Microsoft.Extensions.Configuration;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task Add(User value)
        {
            var connection = await ConnectionDb();
         
            await connection.ExecuteAsync(UserQuery.ADD,value);
        }

        public async Task<User> Get(int id)
        {
            
            var connection = await ConnectionDb();
            return await connection.QueryFirstAsync<User>(UserQuery.GET, new {id});
        }

        public async Task<User> Login(User user)
        {
            var connection = await ConnectionDb();
            return await connection.QueryFirstOrDefaultAsync<User>(UserQuery.LOGIN, user);
        }

        public async Task <List<User>> List()
        {            
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<User>(UserQuery.GET_LIST))?
                .ToList();
        }

        public async Task Remove(int id)
        {            
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(UserQuery.REMOVE, new {id});
        }

        public async Task Update(User value)
        {           
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(UserQuery.UPDATE,value);
        }

        public async Task AddUserToRole(int userID , int roleID)
        {
            var connection = await ConnectionDb();

            await connection.ExecuteAsync(UserQuery.UserRoleADD, new {UserID = userID , RoleID = roleID});
        }

        public async Task<List<User>> GetAllByCompanyId(int companyId)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<User>(UserQuery.GET_LIST_COMPANYID, new { companyId }))?
                .ToList();
        }

        public async Task<User>? UserCheck(User user)
        {
            var connection = await ConnectionDb();
            var value = await connection.QueryFirstOrDefaultAsync<User>(UserQuery.USER_NAME_CHECK, new { UserName = user.UserName });
            return value;
        }

        public async Task<User>? RoleCheck(int roleID, int UserID)
        {
            var connection = await ConnectionDb();
            var value = await connection.QueryFirstOrDefaultAsync<User>(UserQuery.ROLE_CHECK, new { RoleID = roleID, UserID = UserID });
            return value;
        }
    }
}
