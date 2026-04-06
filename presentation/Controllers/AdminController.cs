using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Errors;
using ServiceAbstraction;
using Shared;

namespace Presentation.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController(IServiceManager _servieManager) : ApiBaseController
    {
        // POST : BaseUrl/api/Admin/AssignRole?email=...&role=....
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(string email,string role)
        {
            var result = await _servieManager.AdminService.AssignRoleAsync(email,role);
            if ( result is null )
                return NotFound(new ApiResponse(404));
            return Ok(result);
        }

        // POST : BaseUrl/api/Admin/ChangeRole?email=...&role=....
        [HttpPost("ChangeRole")]
        public async Task<IActionResult> ChangeRole(string email,string role)
        {
            var result = await _servieManager.AdminService.ChangeRoleAsync(email,role);
            if ( result is null )
                return BadRequest(new ApiResponse(400));
            return Ok(result);
        }

        // GET : BaseUrl/api/Admin
        [HttpGet]
        public async Task<ActionResult<List<UserWithRoleDto>>> GetUsersWithRoles()
        {
            var usersWithRoles = await _servieManager.AdminService.GetUsersWithRolesAsync();
            return Ok(usersWithRoles);
        }

        // DELETE : BaseUrl/api/Admin/DeleteRole?email=...&role=....
        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> RemoveRoleFromUser(string email,string role)
        {
            var result = await _servieManager.AdminService.RemoveRoleAsync(email,role);
            if ( result is null )
                return BadRequest(new ApiResponse(400));
            return Ok(result);
        }
    }
}
