using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Abstract
{
    public interface INotificationQueueRepository : IRepository<NotificationQueue>
    {
        Task<List<NotificationQueue>> GetAllByCompanyId(int companyId);
        Task<List<NotificationQueue>> Notification_Get_List_UserID(int userID);
        Task Notification_Update(DateTime dateTime);
    }
}
