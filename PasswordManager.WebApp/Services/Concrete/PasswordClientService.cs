using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;
using System.Net.Http.Headers;
using System.Text;

namespace PasswordManager.WebApp.Services.Concrete
{
    public class PasswordClientService : BaseService<Password> ,IPasswordClientService
    {
        public PasswordClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, httpContextAccessor)
        {
            
        }

        public async Task Add(Password value)
        {
            tokenAuth();
            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            //post isteği
            await _httpClient.PostAsync("AddPassword", content);
        }

       

        public async Task AddUserToPasswordAcces(int passwordID, int userID, int roleID)
        {
            tokenAuth();
            string url = $"AddUserToPassword?passwordID={passwordID}&userID={userID}&roleID={roleID}";
            await _httpClient.PostAsync(url, null);
        }

        public async Task<Password> Get(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetPassword/?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var getPassword = JsonConvert.DeserializeObject<Password>(jsonString);
            return getPassword;
        }

        public async Task<List<Password>> GetAll()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync("GetAllPassword");
            var jsonString = await response.Content.ReadAsStringAsync();
            var Allpassword = JsonConvert.DeserializeObject<List<Password>>(jsonString);
            return Allpassword;
        }

        public async Task<List<Password>> GetAllByCompanyId(int companyId)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetAllBYCompanyIDPassword?companyId={companyId}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var Allpassword = JsonConvert.DeserializeObject<List<Password>>(jsonString);
            return Allpassword;
        }

        public async Task Remove(int id)
        {
            tokenAuth();
           await _httpClient.DeleteAsync($"RemovePassword?id={id}");
        }

        public async Task Update(Password value)
        {
            tokenAuth();
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("UpdatePassword", content);
        }
       
    }
}
