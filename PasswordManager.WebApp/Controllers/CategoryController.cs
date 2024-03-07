using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.WebApp.Models;
using PasswordManager.WebApp.Services.Abstract;

namespace PasswordManager.WebApp.Controllers
{
    

    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }       

        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> AddCategory([FromBody] CategoryResponse category)
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
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryResponse category)
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
