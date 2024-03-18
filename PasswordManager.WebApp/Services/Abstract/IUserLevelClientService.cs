using PasswordManager.Core.Entity;

namespace PasswordManager.WebApp.Services.Abstract
{
    public interface IUserLevelClientService : IBaseService<UserLevel>
    {
        Task<List<UserLevel>> GetAllByCompanyId(int companyId);
    }
}
