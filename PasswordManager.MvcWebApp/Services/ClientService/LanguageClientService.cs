using Newtonsoft.Json;
using PasswordManager.Core.Entity;

namespace PasswordManager.MvcWebApp.Services.ClientService
{
    public class LanguageClientService : ILanguageClientService
    {
        private readonly HttpClient _httpClient;

        public LanguageClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;           
           
        }

        public async Task<List<Language>> GetAllLanguages()
        {
            var response = await _httpClient.GetAsync("https://localhost:7014/api/Language/GetAllLanguages");
            response.EnsureSuccessStatusCode(); // Bu satır, isteğin başarılı olup olmadığını kontrol eder.

            var content = await response.Content.ReadAsStringAsync();
            var languages = JsonConvert.DeserializeObject<List<Language>>(content);

            return languages;
        }
    }
}
