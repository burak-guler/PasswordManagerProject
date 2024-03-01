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
        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPassword()
        {
            var values = await _passwordService.GetPasswordList();
            return Ok(values);
        }

        [HttpGet]
        public async Task<IActionResult> GetPassword(int id)
        {
            var value = await _passwordService.GetPassword(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> AddPassword(Password password)
        {
            await _passwordService.PasswordAdd(password);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePassword(Password password)
        {
            await _passwordService.PasswordUpdate(password);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemovePassword(int id)
        {
            await _passwordService.PasswordRemove(id);
            return Ok();
        }
    }
}
