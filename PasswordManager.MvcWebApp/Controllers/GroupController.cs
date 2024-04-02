using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using PasswordManager.MvcWebApp.Languages;
using PasswordManager.MvcWebApp.Models;
using PasswordManager.MvcWebApp.UrlStatic;
using System.Text;
using System.Text.RegularExpressions;
using static PasswordManager.Core.Entity.Role;

namespace PasswordManager.MvcWebApp.Controllers
{
    public class GroupController : BaseController
    {
        public GroupController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,IConfiguration configuration, IStringLocalizer<Lang> stringLocalizer) : base(httpClient, httpContextAccessor, configuration, stringLocalizer)
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
                var response = await _httpClient.GetAsync($"{ClientUrlHelper.GroupService}GetAllBYCompanyIDGroup?companyId={CurrentUser.CompanyID}");
                var jsonString = await response.Content.ReadAsStringAsync();
                var group = JsonConvert.DeserializeObject<List<GroupViewModels>>(jsonString);
                return View(group);
            }
            else
            {
                return RedirectToAction("UnauthorizedPage", "Error");
            }           
        }

        public async Task<IActionResult> GroupAdd(GroupViewModels group)
        {          

            tokenAuth();

            group.CompanyID = CurrentUser.CompanyID;
            group.LangID = 1;
            group.CreationDate = DateTime.Now;

            var content =  new StringContent(JsonConvert.SerializeObject(group), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{ClientUrlHelper.GroupService}AddGroup", content);


           

            return RedirectToAction("GroupManagement");
        }

        [HttpGet]
        public async Task<IActionResult> GroupUpdate(int id)
        {
            try
            {
                tokenAuth();

                var response = await _httpClient.GetAsync($"{ClientUrlHelper.GroupService}GetGroup/?id={id}");
                var jsonString = await response.Content.ReadAsStringAsync();
                var group = JsonConvert.DeserializeObject<GroupViewModels>(jsonString);


                return View(group);
            }
            catch (Exception ex)
            {

                throw new Exception("HATA:" + ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> GroupUpdate(GroupViewModels group)
        {
            try
            {
                tokenAuth();   
                var content = new StringContent(JsonConvert.SerializeObject(group), Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{ClientUrlHelper.GroupService}UpdateGroup", content);

                return RedirectToAction("GroupManagementDetail", new { id = group.GroupID });
            }
            catch (Exception ex)
            {

                throw new Exception("HATA:" + ex.Message);
            }

        }

        public async Task<IActionResult> GroupRemove(int id)
        {
            tokenAuth();
            var response = await _httpClient.DeleteAsync($"{ClientUrlHelper.GroupService}RemoveGroup?id={id}");
            return RedirectToAction("GroupManagement");
        }


        public async Task<IActionResult> GroupManagementDetail(int id)
        {
            GroupDetailViewModels model = new GroupDetailViewModels();  
            tokenAuth();
            var response = await _httpClient.GetAsync($"{ClientUrlHelper.GroupService}GetGroup/?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<GroupViewModels>(jsonString);
            model.groupViewModels = group;

            var userresponse = await _httpClient.GetAsync($"{ClientUrlHelper.GroupService}UserGroup_BYGroupID/?groupID={id}");
            var userjsonString = await userresponse.Content.ReadAsStringAsync();
            var userList = JsonConvert.DeserializeObject<List<UserViewModels>>(userjsonString);
            model.userViewsModels = userList;

            var roleresponse = await _httpClient.GetAsync($"{ClientUrlHelper.GroupService}GetAllGroupRoleByGroupID/?groupID={id}");
            var rolejsonString = await roleresponse.Content.ReadAsStringAsync();
            var groupRole = JsonConvert.DeserializeObject<List<GroupRoleViewModel>>(rolejsonString);

            List<RoleViewsModels> roleList = new List<RoleViewsModels>();
            foreach (var item in groupRole)
            {
                foreach (UserRole role in Role.AllRoles)
                {

                    if ((int)role == item.RoleID)
                    {
                        roleList.Add(new RoleViewsModels()
                        {

                            RoleID = item.RoleID,
                            GroupRoleID = item.GroupRoleID,
                            RoleName = role.ToString(),
                        });
                    }
                }
            }
            model.roleViewModels = roleList;

            ViewBag.GroupID = id;

            //ROLE

            List<SelectListItem> roleListItem = new List<SelectListItem>();

            foreach (UserRole role in Role.AllRoles)
            {
                roleListItem.Add(new SelectListItem
                {
                    Text = role.ToString(),
                    Value = ((int)role).ToString() // RoleID olarak enumun sayısal değerlerini kullanıyoruz
                });
            }

            ViewBag.Roles = roleListItem;

            //ROLE

            //User

            var companyResponse = await _httpClient.GetAsync($"{ClientUrlHelper.UserService}GetAllBYCompanyIDUser/?companyId={CurrentUser.CompanyID}");
            var companyJsonstring = await companyResponse.Content.ReadAsStringAsync();
            var usersList = JsonConvert.DeserializeObject<List<UserViewModels>>(companyJsonstring);
            ViewBag.User = usersList;

            List<SelectListItem> userListItem = new List<SelectListItem>();
            foreach (var item in usersList)
            {
                userListItem.Add(new SelectListItem
                {
                    Text = item.UserName.ToString(),
                    Value = item.UserID.ToString()
                });
            }
            ViewBag.User = userListItem;
            //User

            return View(model);
        }

        public async Task<IActionResult> GroupToUserAdd(int userID, int groupID)
        {
            try
            {
                tokenAuth();

                var response = await _httpClient.PostAsync($"{ClientUrlHelper.GroupService}AddUserToGroup?userID={userID}&groupID={groupID}", null);

                return RedirectToAction("GroupManagementDetail", new { id = groupID });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> GroupToUserRemove(int userGroupID, int groupID)
        {

            try
            {
                tokenAuth();

                var response = await _httpClient.DeleteAsync($"{ClientUrlHelper.GroupService}RemoveUserToGroup?userGroupID={userGroupID}");

                return RedirectToAction("GroupManagementDetail", new { id = groupID });
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IActionResult> GroupToRoleAdd(int groupID, int roleID)
        {
            try
            {
                tokenAuth();

                var response = await _httpClient.PostAsync($"{ClientUrlHelper.GroupService}AddRoleToGroup?groupID={groupID}&roleID={roleID}", null);

                return RedirectToAction("GroupManagementDetail", new { id = groupID });
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IActionResult> GroupToRoleRemove(int groupRoleID, int groupID)
        {

            try
            {
                tokenAuth();

                var response = await _httpClient.DeleteAsync($"{ClientUrlHelper.GroupService}RemoveGroupToRole?groupRoleID={groupRoleID}");

                return RedirectToAction("GroupManagementDetail", new { id = groupID });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
