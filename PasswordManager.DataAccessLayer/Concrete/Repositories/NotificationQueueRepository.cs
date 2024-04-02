using Dapper;
using Microsoft.Extensions.Configuration;
using PasswordManager.Core.Entity;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Repositories
{
    public class NotificationQueueRepository : GenericRepository<NotificationQueue> ,INotificationQueueRepository
    {
        public NotificationQueueRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task Add(NotificationQueue value)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(NotificationQueueQuery.ADD, value);
        }

        public async Task<NotificationQueue> Get(int id)
        {
            var connection = await ConnectionDb();
            return await connection.QueryFirstOrDefaultAsync<NotificationQueue>(NotificationQueueQuery.GET, new { id });
        }

        public async Task<List<NotificationQueue>> GetAllByCompanyId(int companyId)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<NotificationQueue>(NotificationQueueQuery.GET_LIST_COMPANYID, new { companyId }))?
                .ToList();
        }

        public async Task<List<NotificationQueue>> List()
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<NotificationQueue>(NotificationQueueQuery.GET_LIST))?
                .ToList();
        }

        public async Task<List<NotificationQueue>> Notification_Get_List_UserID(int userID)
        {
            var connection = await ConnectionDb();
            return (await connection.QueryAsync<NotificationQueue>(NotificationQueueQuery.NOTIFICATION_GET_LIST_USERID, new { UserID =userID }))?
                .ToList();
        }

        public async Task Notification_Update(DateTime dateTime)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(NotificationQueueQuery.NOTIFICATION_UPDATE,new {SentDate = dateTime});
        }

        public async Task Remove(int id)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(NotificationQueueQuery.REMOVE, new { id });
        }

        public async Task Update(NotificationQueue value)
        {
            var connection = await ConnectionDb();
            await connection.ExecuteAsync(NotificationQueueQuery.UPDATE, value);
        }
    }
}
