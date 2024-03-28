
using Dapper;
using Microsoft.Extensions.Configuration;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Query;
using System.ComponentModel.Design;
using static PasswordManager.Core.Entity.Role;


namespace PasswordManager.DataAccessLayer.Concrete.Repositories
{
    public class UserRepository : GenericRepository<UserViewModels>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task Add(UserViewModels value)
        {
            var connection = await ConnectionDb();
         
            await connection.ExecuteAsync(UserQuery.ADD,value);
        }

        public async Task<UserViewModels> Get(int id)
        {
            
            var connection = await ConnectionDb();
            return await connection.QueryFirstAsync<UserViewModels>(UserQuery.GET, new {id});
        }

        public async Task<UserViewModels> Login(UserViewModels user)
        {
            var connection = await ConnectionDb();
            return await connection.QueryFirstOrDefaultAsync<UserViewModels>(UserQuery.LOGIN, user);
        }

        public async Task <List<UserViewModels>> List()
        {            
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<UserViewModels>(UserQuery.GET_LIST))?
                .ToList();
        }

        public async Task Remove(int id)
        {            
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(UserQuery.REMOVE, new {id});
        }

        public async Task Update(UserViewModels value)
        {           
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(UserQuery.UPDATE,value);
        }

        public async Task AddUserToRole(int userID , int roleID)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(UserQuery.UserRoleADD, new {UserID = userID , RoleID = roleID});
        }

        public async Task<List<UserViewModels>> GetAllByCompanyId(int companyId)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<UserViewModels>(UserQuery.GET_LIST_COMPANYID, new { CompanyID=companyId }))?
                .ToList();
        }

        public async Task<UserViewModels>? UserCheck(UserViewModels user)
        {
            var connection = await ConnectionDb();
            var value = await connection.QueryFirstOrDefaultAsync<UserViewModels>(UserQuery.USER_NAME_CHECK, new { UserName = user.UserName });
            return value;
        }

        public async Task<UserViewModels>? RoleCheck(int roleID, int UserID)
        {
            var connection = await ConnectionDb();
            var value = await connection.QueryFirstOrDefaultAsync<UserViewModels>(UserQuery.ROLE_CHECK, new { RoleID = roleID, UserID = UserID });
            return value;
        }

        public async Task RemoveUserToRole(int userRoleID)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(UserQuery.UserRoleRemove, new { UserRoleID = userRoleID});
        }

        public async Task<List<UserRoleViewModels>> GetAllUserRoleByUserID(int userID)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<UserRoleViewModels>(UserQuery.UserRole_GetLıst_UserID, new { UserID = userID }))?
                .ToList();
        }
    }
}
