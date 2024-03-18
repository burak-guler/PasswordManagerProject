using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.BusinessLayer.Concrete;
using PasswordManager.Core.Entity;

namespace WebApi.Controllers
{

    public class CompanyController : BaseController
    {
        private ICompanyService _companyService;
        private readonly ILog _logger;
        public CompanyController(IHttpContextAccessor contextAccessor, IMemoryCache memoryCache,ICompanyService companyService, ILog log) : base(contextAccessor, memoryCache)
        {
            _companyService = companyService;
            _logger = log;  
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompany()
        {
            try
            {

                var values = await _companyService.GetAll();
                return Ok(values);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetAllCompany:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var Company = await _companyService.GetById(id);
                if (Company == null)
                {
                    return NotFound();
                }
                return Ok(Company);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetCompany:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }


        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(Company company)
        {
            try
            {
                await _companyService.Add(company);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-AddCompany:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany(Company company)
        {
            try
            {
                await _companyService.Update(company);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-UpdateCompany:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCompany(int id)
        {
            try
            {
                await _companyService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-RemoveCompany:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }


        }
    }
}
