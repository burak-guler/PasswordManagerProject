﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PasswordManager.MvcWebApp.Languages;

namespace PasswordManager.MvcWebApp.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IStringLocalizer<Lang> stringLocalizer) : base(httpClient, httpContextAccessor, configuration,stringLocalizer)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
