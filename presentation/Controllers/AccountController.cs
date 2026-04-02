using Microsoft.AspNetCore.Mvc;
using Presentation.Errors;
using ServiceAbstraction;
using Shared.AccountDto;

namespace Presentation.Controllers
{
    
    public class AccountController(IServiceManager _serviceManager) : ApiBaseController
    {

        // POST : BaseUrl/api/Account/register
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            var user = await _serviceManager.UserService.RegisterAsync(dto);
            return Ok(user);
        }

        // POST : BaseUrl/api/Account/login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var user = await _serviceManager.UserService.LoginAsync(dto);
            if ( user is null )
                return Unauthorized(new ApiResponse(401));
            return Ok(user);
        }
    }
}
