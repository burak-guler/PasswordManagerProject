using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;

namespace WebApi.Controllers
{

    public class UserController : BaseController
    {
        private IUserService _userService;
        private ITokenService _tokenService;
        private ILog _logger;

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor,ITokenService tokenService ,ILog log,IMemoryCache memoryCache) : base(httpContextAccessor,memoryCache)
        {
            _userService = userService;
            _tokenService = tokenService;
            _logger = log;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] User request)
        {
            try
            {
                
                if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
                {
                    return StatusCode(500);
                }

                var user = await _userService.Login(request);
                if (user == null)
                    return StatusCode(500);

                var tokenResponse = _tokenService.GenerateToken(user);                

                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-LoginUser:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var tokenUser = CurrentUser;
                var values = await _userService.GetUserList();
                return Ok(values);
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-GetAllUsers:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var value = await _userService.GetUser(id);
                if (value == null)
                {
                    return NotFound();
                }
                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-GetUser:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            try
            {
                await _userService.UserAdd(user);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-AddUser:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            try
            {
                await _userService.UserUpdate(user);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-UpdateUser:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUser(int id)
        {
            try
            {
                await _userService.UserRemove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-RemoveUser:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }
    }
}
