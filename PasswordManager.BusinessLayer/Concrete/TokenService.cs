using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models;

namespace PasswordManager.BusinessLayer.Concrete
{
    public class TokenService : ITokenService
    {
        
        private JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {            
            _jwtSettings= jwtSettings.Value;    
        }

        public LoginResponse GenerateToken(User user)
        {
            if (string.IsNullOrEmpty(_jwtSettings.Secret) || string.IsNullOrEmpty(_jwtSettings.ValidIssuer) || string.IsNullOrEmpty(_jwtSettings.ValidAudience) || string.IsNullOrEmpty(_jwtSettings.Expires))
            {
                throw new ArgumentNullException(nameof(_jwtSettings));
            }
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));

            var claims = new List<Claim>()
            {
                new Claim("UserName",user.UserName),
                new Claim("Password",user.Password),
                new Claim("UserID",user.UserID.ToString())
            };

            var dateTimeNow = DateTime.UtcNow;

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                claims: claims,
                notBefore: dateTimeNow,
                expires: dateTimeNow.AddDays(Convert.ToDouble(_jwtSettings.Expires)),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );

            return new LoginResponse
            {
                AuthToken = new JwtSecurityTokenHandler().WriteToken(jwt),
                AccessTokenExpireDate = dateTimeNow.AddDays(Convert.ToDouble(_jwtSettings.Expires)),
                AuthenticateResult = true
            };
        }
    }
}
