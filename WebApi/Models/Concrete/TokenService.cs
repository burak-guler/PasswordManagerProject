using Microsoft.IdentityModel.Tokens;
using PasswordManager.Core.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models.Abstract;

namespace WebApi.Models.Concrete
{
    public class TokenService : ITokenService
    {

        readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task<GenerateTokenResponse> GenerateToken(User user)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["AppSettings:Secret"]));

            var claims = new List<Claim>()
            {
                new Claim("UserName",user.UserName),
                new Claim("Password",user.Password),
                new Claim("UserID",user.UserID.ToString())
            };

            var dateTimeNow = DateTime.UtcNow;

            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: configuration["AppSettings:ValidIssuer"],
                    audience: configuration["AppSettings:ValidAudience"],
                    claims: claims,
                    notBefore: dateTimeNow,
                    expires: dateTimeNow.AddDays(3),
                    signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );

            return Task.FromResult(new GenerateTokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                TokenExpireDate = dateTimeNow.AddDays(3)
            });
        }
    }
}
