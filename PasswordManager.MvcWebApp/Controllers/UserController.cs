using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using PasswordManager.MvcWebApp.Services;
using PasswordManager.MvcWebApp.Models;
using PasswordManager.MvcWebApp.UrlStatic;
using System.Text;
using static PasswordManager.Core.Entity.Role;
using PasswordManager.MvcWebApp.Encryption;


namespace PasswordManager.MvcWebApp.Controllers
{
    public class UserController : BaseController
    {        
        IHttpContextAccessor _contextAccessor;
        public UserController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,IConfiguration configuration) : base(httpClient, httpContextAccessor,configuration)
        {
            _contextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
       {                 

            var cookieValue = Request.Cookies["ID"];            

            if (cookieValue != null)
            {
                var userID = CookieEncryptionHelper.Decrypt(cookieValue);
                tokenAuth();
                var response = await _httpClient.GetAsync($"{ClientUrlHelper.UserService}GetUser/?id={userID}");
                var jsonString = await response.Content.ReadAsStringAsync();
                var User = JsonConvert.DeserializeObject<UserViewModels>(jsonString);

                await Login(User);
                return RedirectToAction("Profile");
            }
           

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserViewModels user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                {
                    return View();
                }                
      
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
       
                var response = await _httpClient.PostAsync($"{ClientUrlHelper.UserService}Login", content);
                var jsonString = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonString);

                if (!response.IsSuccessStatusCode) 
                {
                    return View();
                }
                

                _contextAccessor.HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(loginResponse));

                var name = CurrentUser.UserName;
               
                var cookieValue = CookieEncryptionHelper.Encrypt(CurrentUser.UserID.ToString());

                HttpContext.Response.Cookies.Append("ID", cookieValue.ToString());

                return RedirectToAction("Profile");               
            }
            catch (Exception ex)
            {
                //hata sayfası oluştur ve oraya yönlendir.
                return View();
            }            
        }

        public async Task<IActionResult> Logout()
        {           
            HttpContext.Response.Cookies.Delete("ID");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
       {
            try
            {
                tokenAuth();
                var response = await _httpClient.GetAsync($"{ClientUrlHelper.UserService}GetUser/?id={CurrentUser.UserID}");
                var jsonString = await response.Content.ReadAsStringAsync();
                var User = JsonConvert.DeserializeObject<UserViewModels>(jsonString);
                return View(User);


            }
            catch (Exception ex)
            {

                throw new Exception("HATA:" + ex.Message);
            }           
        }

        public async Task<IActionResult> Notification()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"{ClientUrlHelper.NotificationQueueService}Notification_Get_List_UserID?userID={CurrentUser.UserID}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var queue = JsonConvert.DeserializeObject<List<NotificationQueue>>(jsonString);
            
            return View(queue);
        }

        // Admin erişebilir actionlar

        public async Task<IActionResult> UserManagement()
        {
            try
            {
                tokenAuth();                

                ViewBag.companyName = CurrentUser.CompanyName;

                if (CurrentUser.LevelName=="Admin") 
                {
                    var companyResponse = await _httpClient.GetAsync($"{ClientUrlHelper.UserService}GetAllBYCompanyIDUser/?companyId={CurrentUser.CompanyID}");
                    var companyJsonstring = await companyResponse.Content.ReadAsStringAsync();
                    var usersList = JsonConvert.DeserializeObject<List<UserViewModels>>(companyJsonstring);                  
                    

                    return View(usersList); 
                }
                else
                {
                    return RedirectToAction("UnauthorizedPage","Error");
                }                
           
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<IActionResult> UserDetail(int id)
        {
            UserDetailViewModels model = new UserDetailViewModels();

            tokenAuth();

            var response = await _httpClient.GetAsync($"{ClientUrlHelper.UserService}GetUser/?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<UserViewModels>(jsonString);
            model.userViewModels = User;

            var groupresponse = await _httpClient.GetAsync($"{ClientUrlHelper.GroupService}UserGroup_BYuserID/?userID={id}");
            var groupjsonString = await groupresponse.Content.ReadAsStringAsync();
            var groupList = JsonConvert.DeserializeObject<List<GroupViewModels>>(groupjsonString);
            model.groupViewModels = groupList;

            var roleresponse = await _httpClient.GetAsync($"{ClientUrlHelper.UserService}GetAllUserRoleByUserID/?userID={id}");
            var rolejsonString = await roleresponse.Content.ReadAsStringAsync();
            var UserRole = JsonConvert.DeserializeObject<List<UserRoleViewModels>>(rolejsonString);
            
            List<RoleViewsModels> roleList = new List<RoleViewsModels>();
            foreach (var item in UserRole)
            {
                foreach (UserRole role in Role.AllRoles)
                {
                    
                    if ((int)role==item.RoleID)
                    {
                        roleList.Add(new RoleViewsModels() { 
                        
                            RoleID = item.RoleID,
                            UserRoleID=item.UserRoleID,
                            RoleName=role.ToString(),
                        });
                    }
                }
            }
            model.roleViewModels= roleList;

            ViewBag.UserID = id;

            //dropdown için viewbaglar
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

            //Group

            var getAllGroupResponse = await _httpClient.GetAsync($"{ClientUrlHelper.GroupService}GetAllBYCompanyIDGroup?companyId={CurrentUser.CompanyID}");
            var getAllGroupjsonString = await getAllGroupResponse.Content.ReadAsStringAsync();
            var getAllgroup = JsonConvert.DeserializeObject<List<GroupViewModels>>(getAllGroupjsonString);
            ViewBag.Group = getAllgroup;

            List<SelectListItem> groupListItem = new List<SelectListItem>();
            foreach (var item in getAllgroup)
            {
                groupListItem.Add(new SelectListItem
                {
                    Text = item.GroupName.ToString(),
                    Value = item.GroupID.ToString()
                });
            }
            ViewBag.Group = groupListItem;
            //Group



            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserAdd()
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"{ClientUrlHelper.UserLevelService}GetAllBYCompanyIDUserLevel?companyId={CurrentUser.CompanyID}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var levelList = JsonConvert.DeserializeObject<List<UserLevel>>(jsonString);
            ViewBag.Level = levelList;

            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> UserAdd(UserViewModels user)
        {
            tokenAuth();

            user.IsActive = true;
            user.CompanyID=CurrentUser.CompanyID;
            user.CreationDate = DateTime.Now;   
            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            //post isteği
            var response = await _httpClient.PostAsync($"{ClientUrlHelper.UserService}AddUser", content);

            return RedirectToAction("UserManagement");
        }

        public async Task<IActionResult> UserRemove(int id)
        {
            tokenAuth();
            var response = await _httpClient.DeleteAsync($"{ClientUrlHelper.UserService}RemoveUser?id={id}");
            return RedirectToAction("UserManagement");
        }

        [HttpGet]
        public async Task<IActionResult> UserUpdate(int id)
        {
            try
            {
                tokenAuth();

                var levelResponse = await _httpClient.GetAsync($"{ClientUrlHelper.UserLevelService}GetAllBYCompanyIDUserLevel?companyId={CurrentUser.CompanyID}");
                var leveljsonString = await levelResponse.Content.ReadAsStringAsync();
                var levelList = JsonConvert.DeserializeObject<List<UserLevel>>(leveljsonString);
                ViewBag.Level = levelList;

                var response = await _httpClient.GetAsync($"{ClientUrlHelper.UserService}GetUser/?id={id}");
                var jsonString = await response.Content.ReadAsStringAsync();
                var User = JsonConvert.DeserializeObject<UserViewModels>(jsonString);
                return View(User);
            }
            catch (Exception ex)
            {

                throw new Exception("HATA:" + ex.Message);
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> UserUpdate(UserViewModels user)
        {
            try
            {
                tokenAuth();
                user.CompanyID = CurrentUser.CompanyID;
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{ClientUrlHelper.UserService}UpdateUser", content);

                return RedirectToAction("UserDetail", new { id = user.UserID });
            }
            catch (Exception ex)
            {

                throw new Exception("HATA:" + ex.Message);
            }

        }
       

        public async Task<IActionResult> UserToGroupAdd(int userID , int groupID)
        {

            try
            {
                tokenAuth();

                var response = await _httpClient.PostAsync($"{ClientUrlHelper.GroupService}AddUserToGroup?userID={userID}&groupID={groupID}", null);

                return RedirectToAction("UserDetail", new {id=userID });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> UserToGroupRemove(int userGroupID,int userID)
        {

            try
            {
                tokenAuth();

                var response = await _httpClient.DeleteAsync($"{ClientUrlHelper.GroupService}RemoveUserToGroup?userGroupID={userGroupID}");

                return RedirectToAction("UserDetail", new { id = userID });
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IActionResult> UserToRoleAdd(int userID, int roleID)
        {
            try
            {
                tokenAuth();

                var response = await _httpClient.PostAsync($"{ClientUrlHelper.UserService}AddUserToRole?userID={userID}&roleID={roleID}", null);

                return RedirectToAction("UserDetail", new { id = userID });
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<IActionResult> UserToRoleRemove(int userRoleID, int userID)
        {

            try
            {
                tokenAuth();

                var response = await _httpClient.DeleteAsync($"{ClientUrlHelper.UserService}RemoveUserToRole?userRoleID={userRoleID}");

                return RedirectToAction("UserDetail", new { id = userID });
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
