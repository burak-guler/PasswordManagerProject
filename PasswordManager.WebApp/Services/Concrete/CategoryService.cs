using Newtonsoft.Json;
using PasswordManager.WebApp.Models;
using PasswordManager.WebApp.Services.Abstract;
using System.Net.Http.Headers;
using System.Text;

namespace PasswordManager.WebApp.Services.Concrete
{
    public class CategoryService : BaseService<CategoryResponse> ,ICategoryService
    {
        private readonly HttpClient _httpClient;        

        public CategoryService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient,httpContextAccessor)
        {
            _httpClient = httpClient;          

            _httpClient.BaseAddress = new Uri("https://localhost:7014/");
        }

        public async Task Add(CategoryResponse value)
        {
            tokenAuth();
            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            //post isteği
            await _httpClient.PostAsync("api/Category/AddCategory", content);
        }

        public async Task<CategoryResponse> Get(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"api/Category/GetCategory?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<CategoryResponse>(jsonString);
            return categories;
        }

        public async Task<List<CategoryResponse>> GetAll()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync("api/Category/GetAllCategory");
            var jsonString = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<CategoryResponse>>(jsonString);
            return categories;
        }

        public async Task Remove(int id)
        {
            tokenAuth();
            await _httpClient.DeleteAsync($"api/Category/RemoveCategory?id={id}");
        }

        public async Task Update(CategoryResponse value)
        {
            tokenAuth();
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync("api/Category/UpdateCategory", content);
        }
    }
}
