using Microsoft.CodeAnalysis.Host;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using PasswordManager.MvcWebApp.UrlStatic;
using System.Globalization;
using System.Net.Http;
using System.Reflection;

namespace PasswordManager.MvcWebApp.Services
{
    public class LanguageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IStringLocalizer _localizer;

        public LanguageService(IStringLocalizerFactory factory, IHttpContextAccessor httpContextAccessor)
        {

            _httpContextAccessor = httpContextAccessor;

            var type = typeof(Lang);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create(nameof(Lang), assemblyName.Name);
        }

        public LocalizedString GetKey(string key)
        {
            var languageJson = _httpContextAccessor.HttpContext.Session.GetString("SelectedLanguage");

            if (!string.IsNullOrEmpty(languageJson))
            {
                var language = JsonConvert.DeserializeObject<Language>(languageJson);

                if (language != null)
                {
                    var ci = new CultureInfo(language.Lang_ISO);

                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                }
            }

            return _localizer.GetString(key);   
        }

       
    }
}
