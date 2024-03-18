using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;
using System.Text;

namespace PasswordManager.WebApp.Services.Concrete
{
    public class LogClientService : BaseService<Log> , ILogClientService
    {
        public LogClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, httpContextAccessor)
        {
        }

        public async Task Add(Log value)
        {
            tokenAuth();

            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            //post isteği
            var response = await _httpClient.PostAsync("AddLog", content);
        }

        public async Task<Log> Get(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetLog/?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var log = JsonConvert.DeserializeObject<Log>(jsonString);
            return log;
        }

        public async Task<List<Log>> GetAll()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync("GetAllLog");
            var jsonString = await response.Content.ReadAsStringAsync();
            var log = JsonConvert.DeserializeObject<List<Log>>(jsonString);
            return log;
        }

        public async Task<List<Log>> GetAllByCompanyId(int companyId)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetAllBYCompanyIDLog?companyId={companyId}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var log = JsonConvert.DeserializeObject<List<Log>>(jsonString);
            return log;
        }
    

        public async Task Remove(int id)
        {
            tokenAuth();
            var response = await _httpClient.DeleteAsync($"RemoveLog?id={id}");
        }

        public async Task Update(Log value)
        {
            tokenAuth();
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("UpdateLog", content);
        }
    }
}
