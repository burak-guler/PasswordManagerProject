using PasswordManager.Core.Entity;

namespace PasswordManager.WebApp.Services.Abstract
{
    public interface IPasswordClientService : IBaseService<Password>
    {
        Task AddUserToPasswordAcces(int passwordID, int userID, int roleID);
        Task<List<Password>> GetAllByCompanyId(int companyId);
    }
}
