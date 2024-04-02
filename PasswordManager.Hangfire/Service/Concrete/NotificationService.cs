using PasswordManager.Hangfire.Service.Abstract;

namespace PasswordManager.Hangfire.Service.Concrete
{
    public class NotificationService : INotificationService
    {
        HttpClient _httpClient;
        public NotificationService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }


        public async Task NotificationQueUpdate()
        {
            var response = await _httpClient.PutAsync("https://localhost:7014/api/NotificationQueue/Notification_Update", null);
        }
    }
}
