using PasswordManager.Core.Entity;

namespace PasswordManager.MvcWebApp.Services.ClientService
{
    public interface ILanguageClientService
    {
        Task<List<Language>> GetAllLanguages();
    }
}
