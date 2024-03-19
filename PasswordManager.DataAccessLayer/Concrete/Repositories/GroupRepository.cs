using Dapper;
using Microsoft.Extensions.Configuration;
using PasswordManager.Core.Entity;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Query;


namespace PasswordManager.DataAccessLayer.Concrete.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task Add(Group value)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(GroupQuery.ADD, value);

            
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

        public async Task<Group> Get(int id)
        {
            var connection = await ConnectionDb();
            return await connection.QueryFirstOrDefaultAsync<Group>(GroupQuery.GET, new { id });
        }

        public async Task<List<Group>> GetAllByCompanyId(int companyId)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<Group>(GroupQuery.GET_LIST_COMPANYID, new {companyId}))?
                .ToList();
        }

        public async Task<List<Group>> List()
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<Group>(GroupQuery.GET_LIST))?
                .ToList();
        }

        public async Task Remove(int id)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(GroupQuery.REMOVE, new { id });
        }

        public async Task Update(Group value)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(GroupQuery.UPDATE, value);
        }

        public async Task<Group>? UserGroupRoleCheck(int userID, int roleID)
        {
            var connection = await ConnectionDb();
            return await connection.QueryFirstOrDefaultAsync<Group>(GroupQuery.USERGROUP_ROLE_CHECK, new { UserID = userID, RoleID = roleID });
        }
    }
}
