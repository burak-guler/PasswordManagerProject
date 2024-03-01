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
         public BaseController()
        {

        }
        protected User CurrentUser => (User) JsonConvert.DeserializeObject(_contextAccessor.HttpContext.Session.Get("CurrentUser").ToString());
    }
}
