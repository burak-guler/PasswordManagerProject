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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var values = await _userService.GetUserList();
            return Ok(values);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            var value = await _userService.GetUser(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            await _userService.UserAdd(user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            await _userService.UserUpdate(user);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUser(int id)
        {
            await _userService.UserRemove(id);
            return Ok();
        }
    }
}
