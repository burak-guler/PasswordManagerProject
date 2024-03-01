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

        public AuthController(IAuthService authService)
        {
           _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] User request)
        {
            var result = await _authService.LoginUser(request);

            return Ok(result);
        }
    }
}
