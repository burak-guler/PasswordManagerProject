using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.BusinessLayer.Concrete;
using PasswordManager.Core.Entity;

namespace WebApi.Controllers
{

    public class LogController : BaseController
    {
        private ILogService _logService;
        private readonly ILog _logger;
        public LogController(IHttpContextAccessor contextAccessor, IMemoryCache memoryCache, ILogService logService, ILog log) : base(contextAccessor, memoryCache)
        {
            _logService = logService;   
            _logger = log;
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

                _logger.Error("HATA-GetAllLog:" + ex.ToString());
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

                _logger.Error("HATA-GetAllBYCompanyIDLog:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLog(int id)
        {
            try
            {
                var log = await _logService.GetById(id);
                if (log == null)
                {
                    return NotFound();
                }
                return Ok(log);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetLog:" + ex.ToString());
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
                _logger.Error("HATA-AddLog:" + ex.ToString());
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

                _logger.Error("HATA-UpdateLog:" + ex.ToString());
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

                _logger.Error("HATA-RemoveLog:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }
    }
}
