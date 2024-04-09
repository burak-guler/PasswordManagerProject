using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using PasswordManager.MvcWebApp.Services;
using System.Globalization;

namespace PasswordManager.MvcWebApp.Controllers
{
    public class LanguageController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LanguageService _localization;
        public LanguageController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, LanguageService localization) : base(httpClient, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
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
            _httpContextAccessor.HttpContext.Session.SetString("SelectedLanguage", JsonConvert.SerializeObject(new Language() { LangID = langID, Lang_ISO = culture }));

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<List<Language>> GetAllLanguages()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync("https://localhost:7014/api/Language/GetAllLanguages");
            response.EnsureSuccessStatusCode(); // Bu satır, isteğin başarılı olup olmadığını kontrol eder.

            var content = await response.Content.ReadAsStringAsync();
            var languages = JsonConvert.DeserializeObject<List<Language>>(content);

            return languages;
        }
    }
}
