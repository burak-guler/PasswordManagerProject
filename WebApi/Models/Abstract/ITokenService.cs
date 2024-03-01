using PasswordManager.Core.Entity;

namespace WebApi.Models.Abstract
{
    public interface ITokenService
    {
        public Task<GenerateTokenResponse> GenerateToken(User user);
    }
}
