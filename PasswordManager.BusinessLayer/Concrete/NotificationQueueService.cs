﻿using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Concrete
{
    public class NotificationQueueService : INotificationQueueService
    {
        private INotificationQueueRepository _notificationQueueRepository;

        public NotificationQueueService(INotificationQueueRepository notificationQueueRepository)
        {
            _notificationQueueRepository = notificationQueueRepository;
        }

        public async Task Add(NotificationQueue entity)
        {
            await _notificationQueueRepository.Add(entity);
        }

        public async Task<List<NotificationQueue>> GetAll()
        {
            return await _notificationQueueRepository.List();
        }

        public async Task<List<NotificationQueue>> GetAllByCompanyId(int companyId)
        {
            return await _notificationQueueRepository.GetAllByCompanyId(companyId); 
        }

        public async Task<NotificationQueue> GetById(int id)
        {
           return await _notificationQueueRepository.Get(id);
        }

        public async Task Remove(int id)
        {
            await _notificationQueueRepository.Remove(id);  
        }

        public async Task Update(NotificationQueue entity)
        {
            await _notificationQueueRepository.Update(entity);  
        }
    }
}
