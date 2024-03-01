using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Entity;
using WebApi.Models;
using WebApi.Models.Abstract;
using WebApi.Models.Concrete;

namespace WebApi.Controllers
{
   
    public class AuthController : BaseController
    {
        private IAuthService _authService;
        private  ILog _logger;

        public AuthController(IAuthService authService, IHttpContextAccessor httpContextAccessor,ILog log) :base(httpContextAccessor)
        {
           _authService = authService;
            _logger = log;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] User request)
        {
            try
            {
                var result = await _authService.LoginUser(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-LoginUser:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
            
        }
    }
}
