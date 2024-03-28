using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasswordManager.Core.Models;
using PasswordManager.MvcWebApp.UrlStatic;

namespace PasswordManager.MvcWebApp.Controllers
{
    public class GroupController : BaseController
    {
        public GroupController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,IConfiguration configuration) : base(httpClient, httpContextAccessor, configuration)
        {
        }

        public async Task< IActionResult> Index()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"{ClientUrlHelper.GroupService}UserGroup_BYuserID/?userID={CurrentUser.UserID}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<List<GroupViewModels>>(jsonString);
            return View(group);
        }

        public async Task<IActionResult> GroupDetail(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"{ClientUrlHelper.GroupService}GetGroup/?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<GroupViewModels>(jsonString);
            ViewBag.Group = group;

            var userResponse = await _httpClient.GetAsync($"{ClientUrlHelper.GroupService}UserGroup_BYGroupID/?groupID={id}");
            var userJsonString = await userResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserViewModels>>(userJsonString);
            return View(users);
        }

        // Admin erişebilir actionlar

        public async Task<IActionResult> GroupManagement()
        {
            if (CurrentUser.LevelName=="Admin")
            {
                tokenAuth();
                var response = await _httpClient.GetAsync($"GetAllBYCompanyIDGroup?companyId={CurrentUser.CompanyID}");
                var jsonString = await response.Content.ReadAsStringAsync();
                var group = JsonConvert.DeserializeObject<List<GroupViewModels>>(jsonString);
                return View(group);
            }
            else
            {
                return RedirectToAction("UnauthorizedPage", "Error");
            }
           
        }
    }
}
