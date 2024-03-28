using Microsoft.AspNetCore.Mvc;

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
