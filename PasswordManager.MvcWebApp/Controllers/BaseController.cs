using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using System.Configuration;
using System.Net.Http.Headers;

namespace PasswordManager.MvcWebApp.Controllers
{
   
    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        public BaseController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClient = httpClient;

            var url = _configuration.GetValue<string>("Application:ApiEndpoint");
            _httpClient.BaseAddress = new Uri($"{url}");
        }       

        protected LoginResponse CurrentUser
        {
            get
            {
                var currentUserJson = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");

                if (currentUserJson != null)
                    return JsonConvert.DeserializeObject<LoginResponse>(currentUserJson);

                return null;
            }
        }

        public async Task tokenAuth()
        {
            var token = CurrentUser.AuthToken;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
