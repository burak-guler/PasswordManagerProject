using log4net;
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
        private ILog _logger;

        public TokenInfoMiddleware(RequestDelegate next , IConfiguration configuration, IHttpContextAccessor httpContextAccessor,ILog log)
        {
            _next = next;
            this.configuration = configuration;
            _contextAccessor = httpContextAccessor;
            _logger = log;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (!context.Request.Path.Value.Equals("/api/Auth/LoginUser"))
                {
                    var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                    if (token == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Yetkilendirme Eksik.");
                        return;
                    }
                    SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["AppSettings:Secret"]));



                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken(token);
                    if (jwtSecurityToken == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }

                    var userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
                    var userNameClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;
                    var passwordClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "Password")?.Value;


                    User user = new User()
                    {
                        UserID = Convert.ToInt32(userIdClaim),
                        UserName = userNameClaim,
                        Password = passwordClaim
                    };

                    string userJson = JsonConvert.SerializeObject(user);                  
                    

                    _contextAccessor.HttpContext.Session.SetString("CurrentUser", userJson);




                }
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-TokenInfoMiddleware:" + ex.ToString());
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return;
            }


            await _next(context);
        }

       
    }
}
