using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;

namespace PasswordManager.WebApp.Controllers
{

    public class UserLevelController : BaseController
    {
        private IUserLevelClientService _userLevelService;
        public UserLevelController(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUserLevel()
        {
            try
            {

                var values = await _userLevelService.GetAll();
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBYCompanyIDUserLevel(int companyId)
        {
            try
            {

                var values = await _userLevelService.GetAllByCompanyId(companyId);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserLevel(int id)
        {
            try
            {
                var userLevel = await _userLevelService.Get(id);
                if (userLevel == null)
                {
                    return NotFound();
                }
                return Ok(userLevel);
            }
            catch (Exception ex)
            {
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
                return StatusCode(500, "hata: " + ex.Message);
            }


        }
    }
}
