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

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _userService = userService;
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

                return StatusCode(500, "hata: " + ex.Message);
            }
        }
    }
}
