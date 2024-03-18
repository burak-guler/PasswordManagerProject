using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;
using System.Text;

namespace PasswordManager.WebApp.Services.Concrete
{
    public class CompanyClientService : BaseService<Company> , ICompanyClientService
    {
        public CompanyClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, httpContextAccessor)
        {
        }

        public async Task Add(Company value)
        {
            tokenAuth();
            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            //post isteği
            await _httpClient.PostAsync("AddCompany", content);
        }

        public async Task<Company> Get(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetCompany?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var company = JsonConvert.DeserializeObject<Company>(jsonString);
            return company;
        }

        public async Task<List<Company>> GetAll()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync("GetAllCompany");
            var jsonString = await response.Content.ReadAsStringAsync();
            var company = JsonConvert.DeserializeObject<List<Company>>(jsonString);
            return company;
        }

        public async Task Remove(int id)
        {
            tokenAuth();
            await _httpClient.DeleteAsync($"RemoveCompany?id={id}");
        }

        public async Task Update(Company value)
        {
            tokenAuth();
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync("UpdateCompany", content);
        }
    }
}
