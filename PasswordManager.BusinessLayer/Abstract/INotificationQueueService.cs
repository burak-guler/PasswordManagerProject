using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Abstract
{
    public interface INotificationQueueService : IBaseService<NotificationQueue>
    {
        Task<List<NotificationQueue>> GetAllByCompanyId(int companyId);
    }
}
