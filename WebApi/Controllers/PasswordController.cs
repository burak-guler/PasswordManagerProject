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
        public PasswordController(IPasswordService passwordService , IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _passwordService = passwordService;
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

                return StatusCode(500, "hata: " + ex.Message);
            }
        }
    }
}
