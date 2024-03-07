using PasswordManager.WebApp.Models;

namespace PasswordManager.WebApp.Services.Abstract
{
    public interface IUserService : IBaseService<UserResponse>
    {
        Task<UserLoginResponse> Login(UserResponse user);
    }
}

