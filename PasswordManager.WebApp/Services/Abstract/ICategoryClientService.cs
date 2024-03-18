using PasswordManager.Core.Entity;

namespace PasswordManager.WebApp.Services.Abstract
{
    public interface ICategoryClientService : IBaseService<Category>
    {
        Task<List<Category>> GetAllByCompanyId(int companyId);
    }
}
