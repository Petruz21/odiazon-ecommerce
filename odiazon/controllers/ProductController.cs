using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using odiazon.dtos.d_productDto;
using odiazon.services.s_product;

namespace odiazon.controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController (IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all/")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await _productService.GetProductById(id));
        }

        [HttpPost("admin/")]
        public async Task<IActionResult> AddNewProduct(AddProductDto newProduct)
        {
            return Ok(await _productService.AddNewProduct(newProduct));
        }

        [HttpDelete("admin/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _productService.DeleteProduct(id));
        }

        [HttpPut("admin/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto product)
        {
            return Ok(await _productService.UpdateProduct(id, product));
        }
        [HttpGet("filtered")]
        public async Task<IActionResult> GetProductByCategory([FromQuery(Name = "categoryId")] int categoryId)
        {
            return Ok(await _productService.GetProductByCategory(categoryId));
        }
    }
}
