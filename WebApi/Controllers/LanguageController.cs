using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PasswordManager.BusinessLayer.Abstract;

namespace WebApi.Controllers
{


    public class LanguageController : BaseController
    {
        private ILanguageService _languageService;
        public LanguageController(IHttpContextAccessor contextAccessor, IMemoryCache memoryCache, ILanguageService languageService) : base(contextAccessor, memoryCache)
        {
            _languageService = languageService;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllLanguages()
        {
            try
            {

                var values = await _languageService.GetAll();
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }
        }
    }
}
