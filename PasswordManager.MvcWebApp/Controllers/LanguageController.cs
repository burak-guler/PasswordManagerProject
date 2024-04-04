using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PasswordManager.MvcWebApp.Services;

namespace PasswordManager.MvcWebApp.Controllers
{
    public class LanguageController : BaseController
    {
        private readonly LanguageService _localization;
        public LanguageController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, LanguageService localization) : base(httpClient, httpContextAccessor, configuration)
        {
            _localization = localization;
        }

        public IActionResult Index()
        {
            //ViewBag.Ornek = _localization.GetKey("UsersManagement").Value;
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            return View();
        }


        //cookie işlemi ile dil kültürünü cookie de tutuyoruz
        public IActionResult ChangeLanguage(string culture, int langID)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
