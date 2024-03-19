using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;

namespace PasswordManager.WebApp.Controllers
{

    public class NotificationQueueController : BaseController
    {
        private INotificationQueueClientService _notificationQueueService;
        public NotificationQueueController(IHttpContextAccessor contextAccessor, INotificationQueueClientService notificationQueueClientService) : base(contextAccessor)
        {
            _notificationQueueService = notificationQueueClientService;
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
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetNotificationQueue(int id)
        {
            try
            {
                var queue = await _notificationQueueService.Get(id);
                if (queue == null)
                {
                    return NotFound();
                }
                return Ok(queue);
            }
            catch (Exception ex)
            {
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
                return StatusCode(500, "hata: " + ex.Message);
            }
        }
    }
}
