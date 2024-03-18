using PasswordManager.Core.Entity;

namespace PasswordManager.WebApp.Services.Abstract
{
    public interface ILogClientService : IBaseService<Log>
    {
        Task<List<Log>> GetAllByCompanyId(int companyId);
    }
}
