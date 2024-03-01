using PasswordManager.Core.Entity;

namespace WebApi.Models.Abstract
{
    public interface IAuthService
    {
        public Task<UserLoginResponse> LoginUser(User request);
    }
}
