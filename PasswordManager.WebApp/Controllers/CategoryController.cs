using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;

namespace PasswordManager.WebApp.Controllers
{
    public class CategoryController : BaseController
    {
        private ICategoryClientService _categoryService;

        public CategoryController(ICategoryClientService categoryService, IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory() 
        {
            try
            {
                var categories = await _categoryService.GetAll();
                return Ok(categories);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBYCompanyIDCategory(int companyId)
        {
            try
            {

                var values = await _categoryService.GetAllByCompanyId(companyId);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _categoryService.Get(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            try
            {
                await _categoryService.Add(category);
                return Ok();
            }
            catch (Exception ex)
            {                
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            try
            {
                await _categoryService.Update(category);
                return Ok();
            }
            catch (Exception ex)
            {                
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            try
            {
                await _categoryService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {                
                return StatusCode(500, "hata: " + ex.Message);
            }


        }

    }
}
