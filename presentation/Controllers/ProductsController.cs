using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.ProductsDto;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        // GET : BaseUrl/api/Products
        // Get all products
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts()
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET : BaseUrl/api/Products/Id
        // Get specific product
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int Id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(Id);
            return Ok(product);
        }

        // POST : BaseUrl/api/Products
        // Add a product
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDto dto)
        {
            var Added = await _serviceManager.ProductService.AddProductAsync(dto);
            return Ok(Added);
        }

        // PUT : BaseUrl/api/Products/Id
        // Update a product
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateProduct(int Id,UpdateProductDto dto)
        {
            var Updated = await _serviceManager.ProductService.UpdateProduct(Id,dto);
            return Ok(Updated);
        }

        // DELETE : BaseUrl/api/Products/Id
        // Delete a product
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var deleted = await _serviceManager.ProductService.DeleteProduct(Id);
            return Ok(deleted);
        }


    }
}
