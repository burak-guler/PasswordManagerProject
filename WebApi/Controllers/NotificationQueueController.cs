using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.BusinessLayer.Concrete;
using PasswordManager.Core.Entity;

namespace WebApi.Controllers
{

    public class NotificationQueueController : BaseController
    {
        private INotificationQueueService _notificationQueueService;
        private readonly ILog _logger;
        public NotificationQueueController(IHttpContextAccessor contextAccessor, IMemoryCache memoryCache, INotificationQueueService notificationQueueService, ILog log) : base(contextAccessor, memoryCache)
        {
            _notificationQueueService = notificationQueueService;
            _logger = log;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotificationQueue()
        {
            try
            {

                var values = await _notificationQueueService.GetAll();
                return Ok(values);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetAllNotificationQueue:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBYCompanyIDNotificationQueue(int companyId)
        {
            try
            {

                var values = await _notificationQueueService.GetAllByCompanyId(companyId);
                return Ok(values);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetAllBYCompanyIDNotificationQueue:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetNotificationQueue(int id)
        {
            try
            {
                var queue = await _notificationQueueService.GetById(id);
                if (queue == null)
                {
                    return NotFound();
                }
                return Ok(queue);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetNotificationQueue:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }


        }

        [HttpPost]
        public async Task<IActionResult> AddNotificationQueue(NotificationQueue notificationQueue)
        {
            try
            {
                await _notificationQueueService.Add(notificationQueue);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-AddNotificationQueue:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateNotificationQueue(NotificationQueue notificationQueue)
        {
            try
            {
                await _notificationQueueService.Update(notificationQueue);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-UpdateNotificationQueue:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveNotificationQueue(int id)
        {
            try
            {
                await _notificationQueueService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-RemoveNotificationQueue:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }
    }
}
