using Microsoft.AspNetCore.Mvc;

namespace PasswordManager.MvcWebApp.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClient, httpContextAccessor, configuration)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
