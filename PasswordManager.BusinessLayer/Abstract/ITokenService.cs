using PasswordManager.Core.Entity;
using WebApi.Models;

namespace PasswordManager.BusinessLayer.Abstract
{
    public interface ITokenService
    {
       LoginResponse GenerateToken(User user);
    }
}
