using Hangfire;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.Hangfire.Models;
using PasswordManager.Hangfire.Service.Abstract;
using System.Diagnostics;

namespace PasswordManager.Hangfire.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private INotificationQueueService _notificationQueueService;

        public HomeController(ILogger<HomeController> logger, INotificationQueueService notificationQueueService)
        {
            _logger = logger;
            _notificationQueueService = notificationQueueService;

        }

        public async Task<IActionResult> Index()
        {
           var datetime = DateTime.Now;
            RecurringJob.AddOrUpdate("PasswordManagerNotification", () =>  _notificationQueueService.Notification_Update(datetime), "*/5 * * * *");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
