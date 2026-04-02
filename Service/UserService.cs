using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.AccountDto;

namespace Service
{
    class UserService(UserManager<AppUser> _userManager,
        ITokenService _tokenService
        ) : IUserService
    {
        public async Task<UserDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if ( user is null && !await _userManager.CheckPasswordAsync(user!,dto.Password) )
                return null;
            var returnedUser = new UserDto()
            {
                Email = user!.Email!,
                Password = dto.Password,
                Token = await _tokenService.CreateToken(user)
            };
            return returnedUser;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            var User = new AppUser()
            {
                Email = dto.Email,
                UserName = dto.UserName,
                PhoneNumber = dto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(User,dto.Password);
            if ( !result.Succeeded )
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new BadRequestException(errors);
            }

            var ReturnedUser = new UserDto()
            {
                Email = dto.Email,
                Password = dto.Password,
                Token = await _tokenService.CreateToken(User)
            };
            return ReturnedUser;
        }
    }
}
