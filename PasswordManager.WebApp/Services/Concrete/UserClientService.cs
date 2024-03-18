using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using PasswordManager.WebApp.Services.Abstract;
using System.Net.Http.Headers;
using System.Text;

namespace PasswordManager.WebApp.Services.Concrete
{
    public class UserClientService : BaseService<User> ,IUserClientService
    {     
        
        public UserClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, httpContextAccessor)
        {
            
        }

        public async Task Add(User value)
        {
            tokenAuth();

            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            //post isteği
            var response = await _httpClient.PostAsync("AddUser", content);
        }

        public async Task AddUserToRole(int userID, int roleID)
        {
            tokenAuth();

            string url = $"AddUserToRole?userID={userID}&roleID={roleID}";
            //post isteği
            var response = await _httpClient.PostAsync(url, null);
        }

        public async Task<User> Get(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetUser/?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<User>(jsonString);
            return User;
        }

        public async Task<List<User>> GetAll()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync("GetAllUsers");
            var jsonString = await response.Content.ReadAsStringAsync();
            var Users = JsonConvert.DeserializeObject<List<User>>(jsonString);
            return Users;
        }

        public async Task<List<User>> GetAllByCompanyId(int companyId)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetAllBYCompanyIDUser?companyId={companyId}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var Users = JsonConvert.DeserializeObject<List<User>>(jsonString);
            return Users;
        }

        public async Task<LoginResponse> Login(User user)
        {
            tokenAuth();
            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            //post isteği
            var response = await _httpClient.PostAsync("Login", content);
            var jsonString = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonString);
            return loginResponse;

        }

        public async Task Remove(int id)
        {
            tokenAuth();
            var response = await _httpClient.DeleteAsync($"RemoveUser?id={id}");
        }

        public async Task Update(User value)
        {
            tokenAuth();
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("UpdateUser", content);
        }
    }
}
