using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;
using System.Text;

namespace PasswordManager.WebApp.Services.Concrete
{
    public class UserLevelClientService : BaseService<UserLevel> ,IUserLevelClientService
    {
        public UserLevelClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, httpContextAccessor)
        {
        }

        public async Task Add(UserLevel value)
        {
            tokenAuth();

            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            //post isteği
            var response = await _httpClient.PostAsync("AddUserLevel", content);
        }

        public async Task<UserLevel> Get(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetUserLevel/?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<UserLevel>(jsonString);
            return User;
        }

        public async Task<List<UserLevel>> GetAll()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync("GetAllUserLevel");
            var jsonString = await response.Content.ReadAsStringAsync();
            var Users = JsonConvert.DeserializeObject<List<UserLevel>>(jsonString);
            return Users;
        }

        public async Task<List<UserLevel>> GetAllByCompanyId(int companyId)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetAllBYCompanyIDUserLevel?companyId={companyId}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var Users = JsonConvert.DeserializeObject<List<UserLevel>>(jsonString);
            return Users;
        }

        public async Task Remove(int id)
        {
            tokenAuth();
            var response = await _httpClient.DeleteAsync($"RemoveUserLevel?id={id}");
        }

        public async Task Update(UserLevel value)
        {
            tokenAuth();
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("UpdateUserLevel", content);
        }
    }
}
