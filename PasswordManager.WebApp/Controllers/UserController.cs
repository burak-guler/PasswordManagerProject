using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;

namespace PasswordManager.WebApp.Controllers
{
    
    public class UserController : BaseController
    {
        private IUserClientService _userService;
        

        public UserController(IUserClientService userService, IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _userService = userService;
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

                var tokenResponse = await _userService.Login(request);
                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var values = await _userService.GetAll();
                //var user = CurrentUser;
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
                var value = await _userService.Get(id);
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
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            try
            {
                await _userService.Add(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            try
            {
                await _userService.Update(user);
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
                await _userService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        //LKP_UserRole Add
        [HttpPost]
        public async Task<IActionResult> AddUserToRole(int userID, int roleID)
        {
            try
            {
                if (roleID > 0 && userID > 0)
                {
                    await _userService.AddUserToRole(userID, roleID);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
