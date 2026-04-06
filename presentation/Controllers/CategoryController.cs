using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Errors;
using ServiceAbstraction;
using Shared.CategoryDto;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController(IServiceManager _serviceManager) : ApiBaseController
    {

        // GET : BaseUrl/api/Categories
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAllCategories()
        {
            var result = await _serviceManager.CategoryService.GetAllCategoriesAsync();
            return Ok(result);
        }

        // GET : BaseUrl/api/Categories/3
        [HttpGet("{Id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryDto>> GetCategory(int Id)
        {
            var category = await _serviceManager.CategoryService.GetCategoryByIdAsync(Id);
            if ( category is null )
                return NotFound(new ApiResponse(404));
            return Ok(category);
        }

        // PUT : BaseUrl/api/Categories
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryDto dto)
        {
            var updated = await _serviceManager.CategoryService.UpdateCategoryAsync(dto);
            if ( updated == 0 )
                return BadRequest(new ApiResponse(400,"Failed to update category"));
            return Ok(updated);
        }

        // POST : BaseUrl/api/Categories
        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryDto dto)
        {
            var added = await _serviceManager.CategoryService.AddProductAsync(dto);
            if ( added == 0 )
                return BadRequest(new ApiResponse(400,"Failed to add category"));
            return Ok(added);
        }

        // DELETE : BaseUrl/api/Categories/3
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            var deleted = await _serviceManager.CategoryService.DeleteCategoryAsync(Id);
            if ( !deleted )
                return BadRequest(new ApiResponse(400,"Failed to delete category"));
            return Ok(deleted);
        }

    }
}
