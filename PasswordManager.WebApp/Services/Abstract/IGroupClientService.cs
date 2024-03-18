using PasswordManager.Core.Entity;

namespace PasswordManager.WebApp.Services.Abstract
{
    public interface IGroupClientService : IBaseService<Group>
    {
        Task AddUserToGroup(int userID, int groupID);
        Task AddGroupToRole(int groupID, int roleID);

        Task<List<Group>> GetAllByCompanyId(int companyId);
    }
}
