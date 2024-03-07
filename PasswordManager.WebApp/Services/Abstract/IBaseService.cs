using PasswordManager.WebApp.Models;

namespace PasswordManager.WebApp.Services.Abstract
{
    public interface IBaseService<T>
    {
        Task<List<T>> GetAll();
        Task Add(T value);
        Task<T> Get(int id);
        Task Remove(int id);
        Task Update(T value);
    }
}
