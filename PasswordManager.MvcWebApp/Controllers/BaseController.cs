using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using PasswordManager.Core.Models;
using PasswordManager.MvcWebApp.Models;
using System.Net.Http.Headers;

namespace PasswordManager.MvcWebApp.Controllers
{

    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        //public readonly IStringLocalizer<Lang> _stringLocalizer;

        public BaseController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClient = httpClient;
            //_stringLocalizer = stringLocalizer;

            var url = _configuration.GetValue<string>("Application:ApiEndpoint");
            _httpClient.BaseAddress = new Uri($"{url}");
        }

        protected LanguageInfo CurrentCulture
        {
            get
            {
                var culture = Thread.CurrentThread.CurrentCulture.Name;
                var name = Thread.CurrentThread.CurrentUICulture.Name;

                if (!string.IsNullOrEmpty(culture))
                {
                    switch (culture)
                    {
                        case "tr-TR":
                            return new LanguageInfo { LangID = 1, Culture = "tr-TR" };
                        case "en-US":
                            return new LanguageInfo { LangID = 2, Culture = "en-US" };
                        case "fr-FR":
                            return new LanguageInfo { LangID = 3, Culture = "fr-FR" };
                        default:                            
                            return new LanguageInfo { LangID = 1, Culture = "tr-TR" };
                    }
                }
                else
                    return null;
            }
        }

        protected LoginResponse CurrentUser
        {
            get
            {
                var currentUserJson = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");

                if (currentUserJson != null)
                    return JsonConvert.DeserializeObject<LoginResponse>(currentUserJson);

                return null;
            }
        }

        public async Task tokenAuth()
        {
            var token = CurrentUser.AuthToken;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
