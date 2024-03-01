using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.BusinessLayer.Concrete;
using PasswordManager.Core.Entity;

namespace WebApi.Controllers
{
    
    public class UserController : BaseController
    {
        private IUserService _userService;
        private ILog _logger;

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor,ILog log) : base(httpContextAccessor)
        {
            _userService = userService;
            _logger = log;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
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
