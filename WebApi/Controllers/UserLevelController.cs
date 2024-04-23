using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.BusinessLayer.Concrete;
using PasswordManager.Core.Entity;

namespace WebApi.Controllers
{
    public class UserLevelController : BaseController
    {
        private IUserLevelService _userLevelService;
        private ILog _logger;
        public UserLevelController(IHttpContextAccessor contextAccessor, IMemoryCache memoryCache, IUserLevelService
            userLevelService, ILog log) : base(contextAccessor, memoryCache)
        {
            _userLevelService = userLevelService;
            _logger = log;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserLevel()
        {
            try
            {

                var values = await _userLevelService.GetAll();
                if (values == null) { return NotFound(); }
                return Ok(values);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetAllUserLevel:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBYCompanyIDUserLevel(int companyId)
        {
            try
            {

                var values = await _userLevelService.GetAllByCompanyId(companyId);
                if (values == null) { return NotFound(); }
                return Ok(values);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetAllBYCompanyIDUserLevel:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserLevel(int id)
        {
            try
            {
                var userLevel = await _userLevelService.GetById(id);
                if (userLevel == null)
                {
                    return NotFound();
                }
                return Ok(userLevel);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetUserLevel:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }


        }

        [HttpPost]
        public async Task<IActionResult> AddUserLevel(UserLevel userLevel)
        {
            try
            {
                await _userLevelService.Add(userLevel);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-AddUserLevel:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserLevel(UserLevel userLevel)
        {
            try
            {
                await _userLevelService.Update(userLevel);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-UpdateUserLevel:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUserLevel(int id)
        {
            try
            {
                await _userLevelService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-RemoveUserLevel:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }


        }
    }
}
