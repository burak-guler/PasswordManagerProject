using log4net;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        private IHttpContextAccessor _contextAccessor;
        private ILog _logger;
        private IMemoryCache _memoryCache;

        public TokenMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, ILog log, IMemoryCache memoryCache)
        {
            _next = next;
            _contextAccessor = httpContextAccessor;
            _logger = log;
            _memoryCache = memoryCache;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (!context.Request.Path.Value.Equals("/api/User/Login") && !context.Request.Path.Value.Equals("/api/Language/GetAllLanguages"))
                {
                    var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                    if (token == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Yetkilendirme Eksik.");
                        return;
                    }

                    if (!_memoryCache.TryGetValue(token, out UserTokenResponse value))
                    {

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


                        UserTokenResponse userTokenResponse = new UserTokenResponse()
                        {
                            UserID = Convert.ToInt32(userId),
                            UserName = userName,
                            Password = password,
                            AuthToken= token
                        };

                        _memoryCache.Set<UserTokenResponse>(token, userTokenResponse);
                    }

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
