using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;
using System.Text;

namespace PasswordManager.WebApp.Services.Concrete
{
    public class GroupClientService : BaseService<Group> ,IGroupClientService
    {
        public GroupClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, httpContextAccessor)
        {
        }

        public async Task Add(Group value)
        {
            tokenAuth();

            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            //post isteği
            var response = await _httpClient.PostAsync("AddGroup", content);
        }

        public async Task AddGroupToRole(int groupID, int roleID)
        {
            tokenAuth();

            string url = $"AddRoleToGroup?groupID={groupID}&roleID={roleID}";
            //post isteği
            var response = await _httpClient.PostAsync(url, null);
        }

        public async Task AddUserToGroup(int userID, int groupID)
        {
            tokenAuth();

            string url = $"AddUserToGroup?userID={userID}&groupID={groupID}";
            //post isteği
            var response = await _httpClient.PostAsync(url, null);
        }

        public async Task<Group> Get(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetGroup/?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<Group>(jsonString);
            return group;
        }

        public async Task<List<Group>> GetAll()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync("GetAllGroup");
            var jsonString = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<List<Group>>(jsonString);
            return group;
        }

        public async Task<List<Group>> GetAllByCompanyId(int companyId)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetAllBYCompanyIDGroup?companyId={companyId}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<List<Group>>(jsonString);
            return group;
        }

        public async Task Remove(int id)
        {
            tokenAuth();
            var response = await _httpClient.DeleteAsync($"RemoveGroup?id={id}");
        }

        public async Task Update(Group value)
        {
            tokenAuth();
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("UpdateGroup", content);
        }
    }
}
