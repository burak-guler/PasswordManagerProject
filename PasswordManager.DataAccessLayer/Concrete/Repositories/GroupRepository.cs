using Dapper;
using Microsoft.Extensions.Configuration;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Query;
using System.Data.SqlClient;


namespace PasswordManager.DataAccessLayer.Concrete.Repositories
{
    public class GroupRepository : GenericRepository<GroupViewModels>, IGroupRepository
    {
        public GroupRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> Add(GroupViewModels value, SqlConnection conn)
        {
            //var connection = await ConnectionDb();
            return await conn.QuerySingleAsync<int>(GroupQuery.ADD, value);
            
        }

        public async Task AddGroupToRole(int groupID, int roleID)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(GroupQuery.GroupRoleADD, new { GroupID =groupID, RoleID = roleID });
        }

        public async Task AddUserToGroup(int userID, int groupID)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(GroupQuery.UserGroupADD, new { UserID = userID, GroupID = groupID });
        }

        public async Task<GroupViewModels> Get(int id)
        {
            var connection = await ConnectionDb();
            return await connection.QueryFirstOrDefaultAsync<GroupViewModels>(GroupQuery.GET, new { GroupID=id });
        }

        public async Task<List<GroupViewModels>> GetAllByCompanyId(int companyId)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<GroupViewModels>(GroupQuery.GET_LIST_COMPANYID, new {companyId}))?
                .ToList();
        }

        public async Task<List<GroupRoleViewModel>> GetAllGroupRoleByGrouID(int groupID)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<GroupRoleViewModel>(GroupQuery.UserRole_GetLıst_GroupID, new {GroupID=groupID}))?
                .ToList();
        }

        public async Task<int> LangAdd(GroupViewModels value, SqlConnection conn)
        {
            return await conn.QuerySingleAsync<int>(GroupQuery.LANG_ADD, value);
        }

        public async Task<int> LangUpdate(GroupViewModels value, SqlConnection conn)
        {
            return await conn.QuerySingleAsync<int>(GroupQuery.LANG_UPDATE, value);
        }

        public async Task<List<GroupViewModels>> List()
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<GroupViewModels>(GroupQuery.GET_LIST))?
                .ToList();
        }

        public async Task Remove(int id)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(GroupQuery.REMOVE, new { id });
        }

        public async Task RemoveGroupToRole(int groupRoleID)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(GroupQuery.GroupRoleRemove, new { GroupRoleID = groupRoleID  });
        }

        public async Task RemoveUserToGroup(int userGroupID)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(GroupQuery.UserGroupRemove, new { UserGroupID = userGroupID});
        }

        public async Task<int> Update(GroupViewModels value, SqlConnection conn)
        {
            //var connection = await ConnectionDb();
            return await conn.QuerySingleAsync<int>(GroupQuery.UPDATE, value);
        }

        public async Task<GroupViewModels>? UserGroupRoleCheck(int userID, int roleID)
        {
            var connection = await ConnectionDb();
            var value = await connection.QueryFirstOrDefaultAsync<GroupViewModels>(GroupQuery.USERGROUP_ROLE_CHECK, new { UserID = userID, RoleID = roleID });

            return value;
        }

        public async Task<List<UserViewModels>> UserGroup_BYGroupID(int groupID)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<UserViewModels>(GroupQuery.UserGroup_BYGroupID, new { GroupID = groupID }))?
                .ToList();
        }

        public async Task<List<GroupViewModels>> UserGroup_BYUserID(int userID)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<GroupViewModels>(GroupQuery.USERGROUP_BYUSERID, new {UserID = userID}))?
                .ToList();
        }
    }
}
