using Newtonsoft.Json;
using PasswordManager.WebApp.Models;
using PasswordManager.WebApp.Services.Abstract;
using System.Net.Http.Headers;
using System.Text;

namespace PasswordManager.WebApp.Services.Concrete
{
    public class UserService : BaseService<UserResponse> ,IUserService
    {
        private readonly HttpClient _httpClient;
        

        public UserService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, httpContextAccessor)
        {
            _httpClient = httpClient;          

            _httpClient.BaseAddress = new Uri("https://localhost:7014/");
        }

        public async Task Add(UserResponse value)
        {
            tokenAuth();

            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            //post isteği
            var response = await _httpClient.PostAsync("api/User/AddUser", content);
        }

        public async Task<UserResponse> Get(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"api/User/GetUser/?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<UserResponse>(jsonString);
            return User;
        }

        public async Task<List<UserResponse>> GetAll()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync("api/User/GetAllUsers");
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
            var response = await _httpClient.PostAsync("api/User/Login", content);
            var jsonString = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<UserLoginResponse>(jsonString);
            return loginResponse;

        }

        public async Task Remove(int id)
        {
            tokenAuth();
            var response = await _httpClient.DeleteAsync($"api/User/RemoveUser?id={id}");
        }

        public async Task Update(UserResponse value)
        {
            tokenAuth();
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/User/UpdateUser", content);
        }
    }
}
