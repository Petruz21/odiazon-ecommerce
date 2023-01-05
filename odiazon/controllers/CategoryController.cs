using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using odiazon.dtos.d_categoryDto;
using odiazon.services.s_category;

namespace odiazon.controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController (ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("all/")]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _categoryService.GetAllCategories());
        }

        [HttpPost("admin/")]
        public async Task<IActionResult> AddNewProduct(AddCategoryDto newProduct)
        {
            return Ok(await _categoryService.AddNewCategory(newProduct));
        }

        [HttpDelete("admin/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _categoryService.DeleteCategory(id));
        }

        [HttpPut("admin/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateCategoryDto category)
        {
            return Ok(await _categoryService.UpdateCategory(id, category));
        }
    }
}
