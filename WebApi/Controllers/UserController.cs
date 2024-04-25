using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Models;

namespace WebApi.Controllers
{

    public class UserController : BaseController
    {
        private IUserService _userService;
        private ITokenService _tokenService;
        private ILog _logger;
        private IUserLevelService _userLevelService;    

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor,ITokenService tokenService ,ILog log,IMemoryCache memoryCache,IUserLevelService userLevelService) : base(httpContextAccessor,memoryCache)
        {
            _userService = userService;
            _tokenService = tokenService;
            _logger = log;
            _userLevelService = userLevelService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserViewModels request)
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
                var values = await _userService.GetAll();
                return Ok(values);
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-GetAllUsers:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
            
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBYCompanyIDUser(int companyId)
        {
            try
            {              

                var values = await _userService.GetAllByCompanyId(companyId);
                return Ok(values);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetAllBYCompanyIDUser:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {                              

                var value = await _userService.GetById(id);
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
        public async Task<IActionResult> AddUser(UserViewModels user)
        {
            try
            {                
                await _userService.Add(user, CurrentUser.UserID);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-AddUser:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserViewModels user)
        {
            try
            {
                var userValue = await _userService.GetById(CurrentUser.UserID);
                if (userValue == null)
                {
                    return NotFound();
                }

                var level = await _userLevelService.GetById(userValue.LevelID);
                if (level.LevelName != "Admin")
                {
                    return NotFound();
                }

                await _userService.Update(user);
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
                var user = await _userService.GetById(CurrentUser.UserID);
                if (user == null)
                {
                    return NotFound();
                }

                var level = await _userLevelService.GetById(user.LevelID);
                if (level.LevelName != "Admin")
                {
                    return NotFound();
                }

                await _userService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-RemoveUser:" + ex.ToString());
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
                    var user = await _userService.GetById(CurrentUser.UserID);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    var level = await _userLevelService.GetById(user.LevelID);
                    if (level.LevelName != "Admin")
                    {
                        return NotFound();
                    }


                    await _userService.AddUserToRole(userID, roleID);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                _logger.Error("Hata-AddUserToRole" + ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserRoleByUserID(int userID)
        {
            try
            {
                var user = await _userService.GetById(CurrentUser.UserID);
                if (user == null)
                {
                    return NotFound(); 
                }

                var level = await _userLevelService.GetById(user.LevelID);
                if (level.LevelName != "Admin")
                {
                    return NotFound();
                }

                var value = await _userService.GetAllUserRoleByUserID(userID);
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


        [HttpDelete]
        public async Task<IActionResult> RemoveUserToRole(int userRoleID)
        {
            try
            {
                var user = await _userService.GetById(CurrentUser.UserID);
                if (user == null)
                {
                    return NotFound();
                }

                var level = await _userLevelService.GetById(user.LevelID);
                if (level.LevelName != "Admin")
                {
                    return NotFound();
                }

                await _userService.RemoveUserToRole(userRoleID);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-RemoveUserToRole:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }
    }
}
