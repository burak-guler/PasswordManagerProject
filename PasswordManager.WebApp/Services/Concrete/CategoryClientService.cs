using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;
using System.Net.Http.Headers;
using System.Text;

namespace PasswordManager.WebApp.Services.Concrete
{
    public class CategoryClientService : BaseService<Category> ,ICategoryClientService
    {
        public CategoryClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient,httpContextAccessor)
        {
        }

        public async Task Add(Category value)
        {
            tokenAuth();
            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            //post isteği
            await _httpClient.PostAsync("AddCategory", content);
        }

        public async Task<Category> Get(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetCategory?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<Category>(jsonString);
            return categories;
        }

        public async Task<List<Category>> GetAll()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync("GetAllCategory");
            var jsonString = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(jsonString);
            return categories;
        }

        public async Task<List<Category>> GetAllByCompanyId(int companyId)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetAllBYCompanyIDCategory?companyId={companyId}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(jsonString);
            return categories;
        }

        public async Task Remove(int id)
        {
            tokenAuth();
            await _httpClient.DeleteAsync($"RemoveCategory?id={id}");
        }

        public async Task Update(Category value)
        {
            tokenAuth();
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync("UpdateCategory", content);
        }

       
    }
}
