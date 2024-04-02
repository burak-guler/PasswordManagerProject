using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;
using System.Text;

namespace PasswordManager.WebApp.Services.Concrete
{
    public class NotificationQueueClientService : BaseService<NotificationQueue> , INotificationQueueClientService
    {
        public NotificationQueueClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, httpContextAccessor)
        {
        }

        public async Task Add(NotificationQueue value)
        {
            tokenAuth();

            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            //post isteği
            var response = await _httpClient.PostAsync("AddNotificationQueue", content);
        }

        public async Task<NotificationQueue> Get(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetNotificationQueue/?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var queue = JsonConvert.DeserializeObject<NotificationQueue>(jsonString);
            return queue;
        }

        public async Task<List<NotificationQueue>> GetAll()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync("GetAllNotificationQueue");
            var jsonString = await response.Content.ReadAsStringAsync();
            var queue = JsonConvert.DeserializeObject<List<NotificationQueue>>(jsonString);
            return queue;
        }

        public async Task<List<NotificationQueue>> GetAllByCompanyId(int companyId)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"GetAllBYCompanyIDNotificationQueue?companyId={companyId}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var queue = JsonConvert.DeserializeObject<List<NotificationQueue>>(jsonString);
            return queue;
        }

        public async Task Remove(int id)
        {
            tokenAuth();
            var response = await _httpClient.DeleteAsync($"RemoveNotificationQueue?id={id}");
        }

        public async Task Update(NotificationQueue value)
        {
            tokenAuth();
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("UpdateNotificationQueue", content);
        }


    }
}
