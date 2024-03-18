using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;

namespace PasswordManager.WebApp.Controllers
{
  
    public class PasswordController : BaseController
    {
        private IPasswordClientService _passwordService;      
        public PasswordController(IPasswordClientService passwordService, IHttpContextAccessor contextAccessor) : base(contextAccessor)
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
        public async Task<IActionResult> GetAllBYCompanyIDPassword(int companyId)
        {
            try
            {

                var values = await _passwordService.GetAllByCompanyId(companyId);
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
        public async Task<IActionResult> AddPassword([FromBody] Password password)
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
        public async Task<IActionResult> UpdatePassword([FromBody] Password password)
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


        //LKP_PasswordAcces Add
        [HttpPost]
        public async Task<IActionResult> AddUserToPassword(int passwordID, int userID, int roleID)
        {
            try
            {
                if (roleID > 0 && userID > 0 && passwordID > 0)
                {
                    await _passwordService.AddUserToPasswordAcces(passwordID, userID, roleID);
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
