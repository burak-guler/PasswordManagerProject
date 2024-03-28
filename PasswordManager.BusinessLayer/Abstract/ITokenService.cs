using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;

namespace PasswordManager.BusinessLayer.Abstract
{
    public interface ITokenService
    {
       LoginResponse GenerateToken(UserViewModels user);
    }
}
