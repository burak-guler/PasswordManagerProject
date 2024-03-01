using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using System.Text.Json.Serialization;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        private IHttpContextAccessor _contextAccessor;
        public BaseController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected User CurrentUser 
        {
            get
            {
                var currentUserJson = _contextAccessor.HttpContext.Session.GetString("CurrentUser");
                if (currentUserJson != null)
                {
                    return JsonConvert.DeserializeObject<User>(currentUserJson);
                }
                return null;
            }
        }
    }
}
