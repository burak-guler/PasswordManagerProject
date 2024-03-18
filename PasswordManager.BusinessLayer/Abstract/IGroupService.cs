using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;

namespace PasswordManager.BusinessLayer.Abstract
{
    public interface IGroupService : IBaseService<Group>
    {
        Task AddUserToGroup(int userID, int groupID);
        Task AddGroupToRole(int groupID, int roleID);

        Task<List<Group>> GetAllByCompanyId(int companyId);
    }
}
