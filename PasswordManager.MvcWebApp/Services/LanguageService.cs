using Microsoft.CodeAnalysis.Host;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using PasswordManager.MvcWebApp.UrlStatic;
using System.Net.Http;
using System.Reflection;

namespace PasswordManager.MvcWebApp.Services
{
    public class LanguageService
    {
        private IStringLocalizer _localizer;

        public LanguageService( IStringLocalizerFactory factory)
        {
         
            var type = typeof(Lang);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create(nameof(Lang), assemblyName.Name);
        }

        public LocalizedString GetKey(string key)
        {
            return _localizer.GetString(key);   
        }

       
    }
}
