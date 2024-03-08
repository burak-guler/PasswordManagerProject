using PasswordManager.WebApp.Models;

namespace PasswordManager.WebApp.Services.Abstract
{
    public interface IUserClientService : IBaseService<UserResponse>
    {
        Task<UserLoginResponse> Login(UserResponse user);
    }
}

