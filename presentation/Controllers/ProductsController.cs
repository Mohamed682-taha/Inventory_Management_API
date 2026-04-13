using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Errors;
using ServiceAbstraction;
using Shared;
using Shared.ProductsDto;

namespace Presentation.Controllers
{
    public class ProductsController(IServiceManager _serviceManager) : ApiBaseController
    {
        // GET : BaseUrl/api/Products?SearchName=Laptop
        // Get all products (Filtration by => Name)
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }

        // GET : BaseUrl/api/Products/Id
        // Get specific product
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int Id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(Id);
            if ( product is null )
                return NotFound(new ApiResponse(404));
            return Ok(product);
        }

        // POST : BaseUrl/api/Products
        // Add a product
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<int>> AddProduct(CreateProductDto dto)
        {
            var Added = await _serviceManager.ProductService.AddProductAsync(dto);
            return Ok(Added);
        }

        // POST : BaseUrl/api/Products/Import
        // Import Csv file
        [HttpPost("Import")]
        public async Task<ActionResult<int>> ImportCsv(IFormFile file)
        {
            if ( file is null || file.Length == 0 )
                return BadRequest(new ApiResponse(400,"Provide a valid Csv file"));
            if ( !file.FileName.EndsWith(".csv") )
                return BadRequest(new ApiResponse(400,"Only Csv files are allowed"));

            var result = await _serviceManager.ProductService.ImportCsv(file);
            return Ok(result);
        }

        // GET : BaseUrl/api/Products/Export
        // Export products to Csv file
        [HttpGet("Export")]
        public async Task<IActionResult> ExportCsv()
        {
            var csvBytes = await _serviceManager.ProductService.ExportToCsv();
            return File(csvBytes,"text/csv","Products.csv");
        }

        // PUT : BaseUrl/api/Products/Id
        // Update a product
        [HttpPut("{Id:int}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<int>> UpdateProduct(int Id,UpdateProductDto dto)
        {
            var Updated = await _serviceManager.ProductService.UpdateProductAsync(Id,dto);
            return Ok(Updated);
        }

        // DELETE : BaseUrl/api/Products/Id
        // Delete a product
        [HttpDelete("{Id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var deleted = await _serviceManager.ProductService.DeleteProductAsync(Id);
            if ( !deleted )
                return NotFound(new ApiResponse(404));
            return Ok(deleted);
        }


    }
}
