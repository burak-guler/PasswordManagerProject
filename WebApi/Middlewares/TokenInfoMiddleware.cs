using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace WebApi.Middlewares
{
    public class TokenInfoMiddleware
    {
        private readonly RequestDelegate _next;
        readonly IConfiguration configuration;
        private IHttpContextAccessor _contextAccessor;

        public TokenInfoMiddleware(RequestDelegate next , IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            this.configuration = configuration;
            _contextAccessor = httpContextAccessor;
        }


        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString();

            if (token == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Yetkilendirme Eksik.");
                return;
            }
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["AppSettings:Secret"]));

            

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            handler.ValidateToken(token,new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                ValidateIssuer = false,
                ValidateAudience = false

            }, out SecurityToken valideToken);

            var jwtToken = (JwtSecurityToken)valideToken;
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserID");
            var userNameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserName");
            var passwordClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "Password");

            User user = new User()
            {
                UserID = Convert.ToInt32(userIdClaim.Value),
                UserName = userNameClaim.Value,
                Password = passwordClaim.Value
            };

            string userJson = JsonConvert.SerializeObject(user);
            // JSON verisini byte dizisine çevir
            byte[] userDataBytes = Encoding.UTF8.GetBytes(userJson);

            _contextAccessor.HttpContext.Session.Set("CurrentUser", userDataBytes);


            await _next(context);
        }

       
    }
}
