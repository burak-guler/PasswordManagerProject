using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;

namespace PasswordManager.WebApp.Controllers
{

    public class LogController : BaseController
    {
        private ILogClientService _logService;
        public LogController(IHttpContextAccessor contextAccessor, ILogClientService logClientService) : base(contextAccessor)
        {
            _logService = logClientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLog()
        {
            try
            {

                var values = await _logService.GetAll();
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBYCompanyIDLog(int companyId)
        {
            try
            {

                var values = await _logService.GetAllByCompanyId(companyId);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLog(int id)
        {
            try
            {
                var log = await _logService.Get(id);
                if (log == null)
                {
                    return NotFound();
                }
                return Ok(log);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }


        }

        [HttpPost]
        public async Task<IActionResult> AddLog(Log log)
        {
            try
            {
                await _logService.Add(log);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateLog(Log log)
        {
            try
            {
                await _logService.Update(log);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveLog(int id)
        {
            try
            {
                await _logService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }
        }
    }
}
