using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;

namespace PasswordManager.WebApp.Controllers
{

    public class CompanyController : BaseController
    {
        private ICompanyClientService _companyService;
        public CompanyController(IHttpContextAccessor contextAccessor, ICompanyClientService companyClientService) : base(contextAccessor)
        {
            _companyService = companyClientService;
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
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var Company = await _companyService.Get(id);
                if (Company == null)
                {
                    return NotFound();
                }
                return Ok(Company);
            }
            catch (Exception ex)
            {
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
                return StatusCode(500, "hata: " + ex.Message);
            }


        }
    }
}
