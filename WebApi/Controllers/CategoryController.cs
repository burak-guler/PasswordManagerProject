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

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        
        [HttpGet]
        public async Task <IActionResult> GetAllCategory()
        {
            var values = await _categoryService.GetCategoryList();
            return Ok(values);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategory(id);
            if (category == null)
            {
                return NotFound(); 
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
           await _categoryService.CategoryAdd(category);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            await _categoryService.CategoryUpdate(category);  
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            await  _categoryService.CategoryRemove(id); 
            return Ok();
        }
    }
}
