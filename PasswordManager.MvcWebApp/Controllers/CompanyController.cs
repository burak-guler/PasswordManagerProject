using Microsoft.AspNetCore.Mvc;

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
