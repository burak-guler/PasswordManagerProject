using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasswordManager.Core.Entity;

namespace PasswordManager.WebApp.Controllers
{

    //[Authorize]
    public class BaseController : Controller
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
                    return JsonConvert.DeserializeObject<User>(currentUserJson);

                return null;
            }
        }
    }
}
