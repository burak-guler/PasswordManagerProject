using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using PasswordManager.MvcWebApp.UrlStatic;
using System.Text;
using static PasswordManager.Core.Entity.Role;

namespace PasswordManager.MvcWebApp.Controllers
{
    public class PasswordController : BaseController
    {
        public PasswordController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClient, httpContextAccessor, configuration)
        {
        }

        public async Task<IActionResult> Index()
        {
            try
            {               

                tokenAuth();
                var response = await _httpClient.GetAsync($"{ClientUrlHelper.PasswordService}GetAllBYUserIDPassword/?userID={CurrentUser.UserID}");
                var jsonString = await response.Content.ReadAsStringAsync();
                var passwordList = JsonConvert.DeserializeObject<List<PasswordViewModels>>(jsonString);

                if (TempData.ContainsKey("PasswordAdd"))
                {
                    ViewBag.PasswordAdd = TempData["PasswordAdd"].ToString();
                    ViewBag.check = TempData["Check"];
                    TempData.Remove("PasswordAdd"); // TempData'yi temizle
                }


                //ROLE Dropdown

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

                //ROLE Dropdown

                //User Dropdown

                var companyResponse = await _httpClient.GetAsync($"{ClientUrlHelper.UserService}GetAllBYCompanyIDUser/?companyId={CurrentUser.CompanyID}");
                var companyJsonstring = await companyResponse.Content.ReadAsStringAsync();
                var usersList = JsonConvert.DeserializeObject<List<UserViewModels>>(companyJsonstring);

                List<SelectListItem> userListItem = new List<SelectListItem>();
                foreach (var item in usersList)
                {
                    userListItem.Add(new SelectListItem
                    {
                        Text = item.UserName.ToString(),
                        Value = item.UserID.ToString()
                    });
                }
                ViewBag.Users = userListItem;

                //User Dropdown

                //passwordAcces de yetkilendirilmiş şifreler

                var PasswordAccesResponse = await _httpClient.GetAsync($"{ClientUrlHelper.PasswordService}PasswordAccesGetList/?userID={CurrentUser.UserID}&roleID={(int)Role.UserRole.PasswordAcces}");
                var PasswordAccesJsonstring = await PasswordAccesResponse.Content.ReadAsStringAsync();
                var passwordAccesUsersList = JsonConvert.DeserializeObject<List<PasswordViewModels>>(PasswordAccesJsonstring);

                
                ViewBag.PasswordAcces = passwordAccesUsersList;  

                return View(passwordList);
            }
            catch (Exception ex)
            {

                throw new Exception("HATA:" + ex.Message);
            }
        }

        public async Task<IActionResult> PasswordAuthorizationAdd(int passwordID,int userID, int roleID)
        {
            tokenAuth();

            var response = await _httpClient.PostAsync($"{ClientUrlHelper.PasswordService}AddUserToPassword?passwordID={passwordID}&userID={userID}&roleID={roleID}",null);

            NotificationQueue notificationQueue = new NotificationQueue()
            {
                UserID = userID,
                CompanyID = CurrentUser.CompanyID,
                CreationDate = DateTime.Now,
                Title = Enum.GetName(typeof(Role.UserRole), roleID),                
                Body = "Rol Yetkilendirme Ekleme"

            };

            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(notificationQueue), Encoding.UTF8, "application/json");
            //post isteği
            var notificationResponse = await _httpClient.PostAsync($"{ClientUrlHelper.NotificationQueueService}AddNotificationQueue", content);

            return RedirectToAction("Index");
        }

        //şifre bazlı verilen rolleri currentuser'ın kim olduğu bilmedğimiz için yapmadık. YAPILABİLİR 
        public async Task<IActionResult> PasswordAuthorizationRemove(int passwordID, int userID, int roleID)
        {
            tokenAuth();

            var response = await _httpClient.DeleteAsync($"{ClientUrlHelper.PasswordService}RemoveUserToPassword?passwordID={passwordID}&userID={userID}&roleID={roleID}");


            NotificationQueue notificationQueue = new NotificationQueue()
            {
                UserID = userID,
                CompanyID = CurrentUser.CompanyID,
                CreationDate = DateTime.Now,
                Title = "Rol Yetkilendirme Silme",
                Body = Enum.GetName(typeof(Role.UserRole), roleID)

            };

            //json a çevirme
            var content = new StringContent(JsonConvert.SerializeObject(notificationQueue), Encoding.UTF8, "application/json");
            //post isteği
            var notificationResponse = await _httpClient.PostAsync($"{ClientUrlHelper.NotificationQueueService}AddNotificationQueue", content);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> PasswordAdd()
        {
            try
            {
                tokenAuth();
                var response = await _httpClient.GetAsync($"{ClientUrlHelper.UserLevelService}GetAllBYCompanyIDUserLevel?companyId={CurrentUser.CompanyID}");
                var jsonString = await response.Content.ReadAsStringAsync();
                var levelList = JsonConvert.DeserializeObject<List<UserLevel>>(jsonString);
                ViewBag.Level = levelList;

                var categoryResponse = await _httpClient.GetAsync($"{ClientUrlHelper.CategoryService}GetAllBYCompanyIDCategory?companyId={CurrentUser.CompanyID}");
                var categoryJsonString = await categoryResponse.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<Category>>(categoryJsonString);
                ViewBag.Category = categories;
                return View();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> PasswordAdd(PasswordViewModels password)
        {
            try
            {
                tokenAuth();
                password.UserID = CurrentUser.UserID;
                password.CompanyID = CurrentUser.CompanyID;
                password.IsActive = true;
                var content = new StringContent(JsonConvert.SerializeObject(password), Encoding.UTF8, "application/json");
                //post isteği
                var response = await _httpClient.PostAsync($"{ClientUrlHelper.PasswordService}AddPassword", content);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["PasswordAdd"] = await response.Content.ReadAsStringAsync();
                    TempData["Check"] = false;
                    return RedirectToAction("Index");
                    
                }

                TempData["PasswordAdd"] = "Şifre Başarılı Bir Şekilde Oluşturuldu.!";
                TempData["Check"] = true;

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        [HttpGet]
        public async Task<IActionResult> PasswordUpdate(int id)
        {
            tokenAuth();
            var response = await _httpClient.GetAsync($"{ClientUrlHelper.PasswordService}GetPassword/?id={id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var getPassword = JsonConvert.DeserializeObject<PasswordViewModels>(jsonString);

            var levelResponse = await _httpClient.GetAsync($"{ClientUrlHelper.UserLevelService}GetAllBYCompanyIDUserLevel?companyId={CurrentUser.CompanyID}");
            var leveljsonString = await levelResponse.Content.ReadAsStringAsync();
            var levelList = JsonConvert.DeserializeObject<List<UserLevel>>(leveljsonString);
            ViewBag.Level = levelList;

            var categoryResponse = await _httpClient.GetAsync($"{ClientUrlHelper.CategoryService}GetAllBYCompanyIDCategory?companyId={CurrentUser.CompanyID}");
            var categoryJsonString = await categoryResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(categoryJsonString);
            ViewBag.Category = categories;

            return View(getPassword);
        }
        [HttpPost]
        public async Task<IActionResult> PasswordUpdate(PasswordViewModels password)
        {
            tokenAuth();
            password.UserID = CurrentUser.UserID;
            password.CompanyID = CurrentUser.CompanyID;
            password.IsActive = true;
            var content = new StringContent(JsonConvert.SerializeObject(password), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"{ClientUrlHelper.PasswordService}UpdatePassword", content);
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> PasswordRemove(int id)
        {
            try
            {
                tokenAuth();
                var response = await _httpClient.DeleteAsync($"{ClientUrlHelper.PasswordService}RemovePassword/?id={id}");
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }            
        }


    }
}
