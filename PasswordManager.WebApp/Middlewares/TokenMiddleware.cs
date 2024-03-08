using log4net;
using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using System.IdentityModel.Tokens.Jwt;

namespace PasswordManager.WebApp.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        private IHttpContextAccessor _contextAccessor;
        private ILog _logger;

        public TokenMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, ILog log)
        {
            _next = next;
            _contextAccessor = httpContextAccessor;
            _logger = log;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (!context.Request.Path.Value.Equals("/User/Login"))
                {
                    var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                    if (token == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Yetkilendirme Eksik.");
                        return;
                    }

                    var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

                    if (jwtSecurityToken == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Yetkilendirme Eksik.");
                        return;
                    }

                    var userId = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
                    var userName = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;
                    var password = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "Password")?.Value;

                    //todo: claim değerlerinin varlığını kontrol et
                    if (userId == null && userName == null && password == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Yetkilendirme Eksik.");
                        return;
                    }


                    User user = new User()
                    {
                        UserID = Convert.ToInt32(userId),
                        UserName = userName,
                        Password = password
                    };

                    _contextAccessor.HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(user));
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
