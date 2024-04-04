using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PasswordManager.MvcWebApp.Services;

namespace PasswordManager.MvcWebApp.Controllers
{
    public class CompanyController : BaseController
    {
        public CompanyController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClient, httpContextAccessor, configuration)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
