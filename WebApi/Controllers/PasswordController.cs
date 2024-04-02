using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Models;

namespace WebApi.Controllers
{

    public class PasswordController : BaseController
    {
        private IPasswordService _passwordService;
        private  ILog _logger;
        private IUserService _userService;
        private IUserLevelService _userLevelService;
        public PasswordController(IPasswordService passwordService , IHttpContextAccessor httpContextAccessor, ILog log, IMemoryCache memoryCache, IUserService userService, IUserLevelService userLevelService) : base(httpContextAccessor, memoryCache)
        {
            _passwordService = passwordService;
            _logger = log;
            _userService = userService;
            _userLevelService = userLevelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPassword()
        {
            try
            {
                var values = await _passwordService.GetAll();
                return Ok(values);
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-GetAllPassword:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllBYCompanyIDPassword(int companyId)
        {
            try
            {
                var user = await _userService.GetById(CurrentUser.UserID);
                if (user == null)
                {
                    throw new UnauthorizedAccessException("Kullanıcı bulunamadı.");
                }

                var level = await _userLevelService.GetById(user.LevelID);
                if (level.LevelName != "Admin")
                {
                    throw new UnauthorizedAccessException("Kullanıcı yetki dışı.");
                }

                var values = await _passwordService.GetAllByCompanyId(companyId);
                return Ok(values);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetAllBYCompanyIDPassword:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBYUserIDPassword(int userID)
        {
            try
            {
                var values = await _passwordService.GetAllByUserId(userID);
                return Ok(values);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetAllBYUserIDPassword:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPassword(int id)
        {
            try
            {
                var value = await _passwordService.GetById(id, CurrentUser.UserID);
                if (value == null)
                {
                    return NotFound();
                }
                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-GetPassword:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }


        }

        [HttpPost]
        public async Task<IActionResult> AddPassword(PasswordViewModels password)
        {
            try
            {                
                await _passwordService.Add(password, CurrentUser.UserID);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Kullanıcı rolü eksik" || ex.Message == "Kullanıcı level seviyesi yetersiz")
                {
                    return StatusCode(403, "Hata: Kullanıcı rolü eksik veya  level seviyesi yetersiz");
                }
                else
                {
                    _logger.Error("HATA-AddPassword:" + ex.ToString());
                    return StatusCode(500, "hata: " + ex.Message); // Diğer hata durumları için HTTP 500 iç sunucu hatası yanıtı döndürülür
                }
            }


        }

        [HttpPut]
        public async Task<IActionResult> UpdatePassword(PasswordViewModels password)
        {
            try
            {
                await _passwordService.Update(password, CurrentUser.UserID);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-UpdatePassword:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemovePassword(int id)
        {
            try
            {
                await _passwordService.Remove(id, CurrentUser.UserID);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-RemovePassword:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        //LKP_PasswordAcces Add
        [HttpPost]
        public async Task<IActionResult> AddUserToPassword(int passwordID,int userID, int roleID)
        {
            try
            {
                if (roleID > 0 && userID > 0 && passwordID > 0)
                {
                    var user = await _userService.GetById(CurrentUser.UserID);
                    if (user == null)
                    {
                        throw new UnauthorizedAccessException("Kullanıcı bulunamadı.");
                    }

                    var level = await _userLevelService.GetById(user.LevelID);
                    if (level.LevelName != "Admin")
                    {
                        throw new UnauthorizedAccessException("Kullanıcı yetki dışı.");
                    }

                    await _passwordService.AddUserToPasswordAcces(passwordID, userID, roleID);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                _logger.Error("Hata-AddUserToPassword" + ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> PasswordAccesGetList(int userID, int roleID)
        {
            try
            {
                var value = await _passwordService.PasswordAccesGetList(userID,roleID);
                if (value == null)
                {
                    return NotFound();
                }
                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-GetPassword:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }


        //LKP_PasswordAcces remove
        [HttpDelete]
        public async Task<IActionResult> RemoveUserToPassword(int passwordID, int userID, int roleID)
        {
            try
            {
                var user = await _userService.GetById(CurrentUser.UserID);
                if (user == null)
                {
                    throw new UnauthorizedAccessException("Kullanıcı bulunamadı.");
                }

                var level = await _userLevelService.GetById(user.LevelID);
                if (level.LevelName != "Admin")
                {
                    throw new UnauthorizedAccessException("Kullanıcı yetki dışı.");
                }

                await _passwordService.RemoveUserToPasswordAcces(passwordID, userID, roleID);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-RemoveUserToPassword:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }
    }
}
