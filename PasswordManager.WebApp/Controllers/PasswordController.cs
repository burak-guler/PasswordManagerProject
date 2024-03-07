using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.WebApp.Models;
using PasswordManager.WebApp.Services.Abstract;

namespace PasswordManager.WebApp.Controllers
{
  
    public class PasswordController : ControllerBase
    {
        private IPasswordService _passwordService;      
        public PasswordController(IPasswordService passwordService) 
        {
            _passwordService = passwordService;
       
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
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetPassword(int id)
        {
            try
            {
                var value = await _passwordService.Get(id);
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
        public async Task<IActionResult> AddPassword([FromBody] PasswordResponse password)
        {
            try
            {
                await _passwordService.Add(password);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }


        }

        [HttpPut]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordResponse password)
        {
            try
            {
                await _passwordService.Update(password);
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
                await _passwordService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }
        }
    }
}
