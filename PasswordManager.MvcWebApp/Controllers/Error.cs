using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PasswordManager.MvcWebApp.Languages;

namespace PasswordManager.MvcWebApp.Controllers
{
    public class Error : BaseController
    {
        public Error(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IStringLocalizer<Lang> stringLocalizer) : base(httpClient, httpContextAccessor, configuration, stringLocalizer)
        {
        }

        public async Task< IActionResult> Index()
        {
            return View();
        }

        public async Task< IActionResult> UnauthorizedPage()
        {
            return View();
        }

    }
}
