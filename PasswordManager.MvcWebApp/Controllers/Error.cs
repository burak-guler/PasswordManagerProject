using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PasswordManager.MvcWebApp.Services;

namespace PasswordManager.MvcWebApp.Controllers
{
    public class Error : BaseController
    {
        public Error(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClient, httpContextAccessor, configuration)
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
