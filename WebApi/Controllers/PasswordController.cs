using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.BusinessLayer.Concrete;
using PasswordManager.Core.Entity;

namespace WebApi.Controllers
{
    
    public class PasswordController : BaseController
    {
        private IPasswordService _passwordService;
        private  ILog _logger;
        public PasswordController(IPasswordService passwordService , IHttpContextAccessor httpContextAccessor, ILog log) : base(httpContextAccessor)
        {
            _passwordService = passwordService;
            _logger = log;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPassword()
        {
            try
            {
                var values = await _passwordService.GetPasswordList();
                return Ok(values);
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-GetAllPassword:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetPassword(int id)
        {
            try
            {
                var value = await _passwordService.GetPassword(id);
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
        public async Task<IActionResult> AddPassword(Password password)
        {
            try
            {
                await _passwordService.PasswordAdd(password);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-AddPassword:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }


        }

        [HttpPut]
        public async Task<IActionResult> UpdatePassword(Password password)
        {
            try
            {
                await _passwordService.PasswordUpdate(password);
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
                await _passwordService.PasswordRemove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-RemovePassword:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }
    }
}
