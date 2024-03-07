using log4net;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;

namespace WebApi.Controllers
{

    public class CategoryController : BaseController
    {
        private ICategoryService _categoryService;
        private readonly ILog _logger;

        public CategoryController(ICategoryService categoryService, IHttpContextAccessor httpContextAccessor, ILog log) : base(httpContextAccessor)
        {
            _categoryService = categoryService;
            _logger = log;
        }

        
        [HttpGet]
        public async Task <IActionResult> GetAllCategory()
        {
            try
            {
                var user = CurrentUser;

                var values = await _categoryService.GetCategoryList();
                return Ok(values);
            }
            catch (Exception ex)
            {
                
                _logger.Error("HATA-GetAllCategory:"+ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _categoryService.GetCategory(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetCategory:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

          
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            try
            {
                await _categoryService.CategoryAdd(category);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-AddCategory:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
           
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            try
            {
                await _categoryService.CategoryUpdate(category);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-UpdateCategory:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            try
            {
                await _categoryService.CategoryRemove(id);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-RemoveCategory:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

           
        }
    }
}
