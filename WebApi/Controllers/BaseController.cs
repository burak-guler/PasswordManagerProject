using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        private IHttpContextAccessor _contextAccessor;
        private IMemoryCache _memoryCache;

        public BaseController(IHttpContextAccessor contextAccessor, IMemoryCache memoryCache)
        {
            _contextAccessor = contextAccessor;
            _memoryCache = memoryCache;
        }        
        
        protected UserTokenResponse CurrentUser
        {
            get
            {
                string token = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }
                if (_memoryCache.TryGetValue<UserTokenResponse>(token, out UserTokenResponse currentUser))
                {                    
                    return currentUser;
                }
                else
                {                    
                    return null;
                }                
            }
        }
        
        
    }
}
