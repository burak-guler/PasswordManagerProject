using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Abstract
{
    public interface IGroupRepository : IRepository<GroupViewModels>
    {
        Task AddUserToGroup(int userID, int groupID);
        Task RemoveUserToGroup(int userGroupID);
        Task AddGroupToRole(int groupID, int roleID);
        Task RemoveGroupToRole(int groupID, int roleID);
        Task<GroupViewModels> UserGroupRoleCheck(int userID, int roleID);
        Task<List<GroupViewModels>> GetAllByCompanyId(int companyId);
        Task<int> Add(GroupViewModels value, SqlConnection conn);
        Task<int> Update(GroupViewModels value, SqlConnection conn);
        Task<int> LangAdd(GroupViewModels value, SqlConnection conn);
        Task<int> LangUpdate(GroupViewModels value, SqlConnection conn);
        Task<List<GroupViewModels>> UserGroup_BYUserID(int userID);
        Task<List<UserViewModels>> UserGroup_BYGroupID(int groupID);
    }
}
