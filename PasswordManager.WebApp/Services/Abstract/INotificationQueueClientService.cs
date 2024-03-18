using PasswordManager.Core.Entity;

namespace PasswordManager.WebApp.Services.Abstract
{
    public interface INotificationQueueClientService : IBaseService<NotificationQueue>
    {
        Task<List<NotificationQueue>> GetAllByCompanyId(int companyId);
    }
}
