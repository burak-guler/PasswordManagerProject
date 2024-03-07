using Newtonsoft.Json;
using PasswordManager.WebApp.Models;
using PasswordManager.WebApp.Services.Abstract;
using System.Net.Http.Headers;
using System.Text;

namespace PasswordManager.WebApp.Services.Concrete
{
    public class PasswordService : BaseService<PasswordResponse> ,IPasswordService
    {
        private readonly HttpClient _httpClient;
        

        public PasswordService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, httpContextAccessor)
        {
            _httpClient = httpClient;        

            _httpClient.BaseAddress = new Uri("https://localhost:7014/");
        }

        public async Task Add(PasswordResponse value)
        {
            tokenAuth();
            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            //post isteği
            await _httpClient.PostAsync("api/Password/AddPassword", content);
        }

        public async Task<PasswordResponse> Get(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"api/Password/GetPassword/?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var getPassword = JsonConvert.DeserializeObject<PasswordResponse>(jsonString);
            return getPassword;
        }

        public async Task<List<PasswordResponse>> GetAll()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync("api/Password/GetAllPassword");
            var jsonString = await response.Content.ReadAsStringAsync();
            var Allpassword = JsonConvert.DeserializeObject<List<PasswordResponse>>(jsonString);
            return Allpassword;
        }

        public async Task Remove(int id)
        {
            tokenAuth();
           await _httpClient.DeleteAsync($"api/Password/RemovePassword?id={id}");
        }

        public async Task Update(PasswordResponse value)
        {
            tokenAuth();
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/Password/UpdatePassword", content);
        }
    }
}
