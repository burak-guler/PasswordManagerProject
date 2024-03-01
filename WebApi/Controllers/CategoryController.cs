using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;

namespace WebApi.Controllers
{
    
    public class CategoryController : BaseController
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _categoryService = categoryService;
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

                return StatusCode(500, "hata: " + ex.Message);
            }

           
        }
    }
}
