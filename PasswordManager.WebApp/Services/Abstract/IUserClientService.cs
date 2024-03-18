using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;

namespace PasswordManager.WebApp.Services.Abstract
{
    public interface IUserClientService : IBaseService<User>
    {
        Task<LoginResponse> Login(User user);
        Task AddUserToRole(int userID, int roleID);

        Task<List<User>> GetAllByCompanyId(int companyId);
    }
}

