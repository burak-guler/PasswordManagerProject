using Newtonsoft.Json;
using PasswordManager.WebApp.Models;
using PasswordManager.WebApp.Services.Abstract;
using System.Net.Http.Headers;
using System.Text;

namespace PasswordManager.WebApp.Services.Concrete
{
    public class PasswordClientService : BaseService<PasswordResponse> ,IPasswordClientService
    {
        public PasswordClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, httpContextAccessor)
        {
            
        }

        public async Task Add(PasswordResponse value)
        {
            tokenAuth();
            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            //post isteği
            await _httpClient.PostAsync("AddPassword", content);
        }

        public async Task<PasswordResponse> Get(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetPassword/?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var getPassword = JsonConvert.DeserializeObject<PasswordResponse>(jsonString);
            return getPassword;
        }

        public async Task<List<PasswordResponse>> GetAll()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync("GetAllPassword");
            var jsonString = await response.Content.ReadAsStringAsync();
            var Allpassword = JsonConvert.DeserializeObject<List<PasswordResponse>>(jsonString);
            return Allpassword;
        }

        public async Task Remove(int id)
        {
            tokenAuth();
           await _httpClient.DeleteAsync($"RemovePassword?id={id}");
        }

        public async Task Update(PasswordResponse value)
        {
            tokenAuth();
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("UpdatePassword", content);
        }
    }
}
