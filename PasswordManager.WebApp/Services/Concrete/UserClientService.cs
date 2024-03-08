using Newtonsoft.Json;
using PasswordManager.WebApp.Models;
using PasswordManager.WebApp.Services.Abstract;
using System.Net.Http.Headers;
using System.Text;

namespace PasswordManager.WebApp.Services.Concrete
{
    public class UserClientService : BaseService<UserResponse> ,IUserClientService
    {     
        
        public UserClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, httpContextAccessor)
        {
            
        }

        public async Task Add(UserResponse value)
        {
            tokenAuth();

            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            //post isteği
            var response = await _httpClient.PostAsync("AddUser", content);
        }

        public async Task<UserResponse> Get(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetUser/?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<UserResponse>(jsonString);
            return User;
        }

        public async Task<List<UserResponse>> GetAll()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync("GetAllUsers");
            var jsonString = await response.Content.ReadAsStringAsync();
            var Users = JsonConvert.DeserializeObject<List<UserResponse>>(jsonString);
            return Users;
        }

        public async Task<UserLoginResponse> Login(UserResponse user)
        {
            tokenAuth();
            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            //post isteği
            var response = await _httpClient.PostAsync("Login", content);
            var jsonString = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<UserLoginResponse>(jsonString);
            return loginResponse;

        }

        public async Task Remove(int id)
        {
            tokenAuth();
            var response = await _httpClient.DeleteAsync($"RemoveUser?id={id}");
        }

        public async Task Update(UserResponse value)
        {
            tokenAuth();
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("UpdateUser", content);
        }
    }
}
