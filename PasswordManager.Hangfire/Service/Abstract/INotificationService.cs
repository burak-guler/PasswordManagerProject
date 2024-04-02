using PasswordManager.Core.Entity;

namespace PasswordManager.Hangfire.Service.Abstract
{
    public interface INotificationService
    {
        Task NotificationQueUpdate();
    }
}
