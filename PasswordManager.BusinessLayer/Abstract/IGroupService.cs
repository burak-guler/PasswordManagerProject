using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;

namespace PasswordManager.BusinessLayer.Abstract
{
    public interface IGroupService : IBaseService<GroupViewModels>
    {
        Task AddUserToGroup(int userID, int groupID, int currentID);
        Task RemoveUserToGroup(int userGroupID, int currentID);
        Task AddGroupToRole(int groupID, int roleID, int currentID);
        Task RemoveGroupToRole(int groupRoleID, int currentID);        
        Task<List<GroupViewModels>> GetAllByCompanyId(int companyId);
        Task<List<GroupViewModels>> UserGroup_BYUserID(int userID);
        Task<List<UserViewModels>> UserGroup_BYGroupID(int groupID);
        Task<List<GroupRoleViewModel>> GetAllGroupRoleByGrouID(int groupID);
    }
}
