using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceAbstraction;
using Shared;

namespace Service
{
    class AdminService(UserManager<AppUser> _userManager,RoleManager<IdentityRole> _roleManager) : IAdminService
    {
        public async Task<string?> AssignRoleAsync(string email,string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if ( user is null )
                return null;
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if ( !roleExists )
                return null;
            var userRoleExist = await _userManager.IsInRoleAsync(user,role);
            if ( userRoleExist )
                return null;
            var result = await _userManager.AddToRoleAsync(user,role);
            if ( !result.Succeeded )
                throw new BadRequestException(result.Errors.Select(e => e.Description));
            return $"Role: {role} is assigned to: {email} successfully";
        }

        public async Task<string?> ChangeRoleAsync(string email,string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if ( user is null )
                return null;
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if ( !roleExists )
                return null;
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user,userRoles);

            var result = await _userManager.AddToRoleAsync(user,role);
            if ( !result.Succeeded )
                throw new BadRequestException(result.Errors.Select(e => e.Description));
            return $"{email} role changed to:{role} successfully";
        }

        public async Task<List<UserWithRoleDto>> GetUsersWithRolesAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var result = new List<UserWithRoleDto>();
            foreach ( var user in users )
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserWithRoleDto
                {
                    Email = user.Email!,
                    UserName = user.UserName!,
                    Roles = roles.ToList()
                });
            }
            return result;
        }

        public async Task<string?> RemoveRoleAsync(string email,string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if ( user is null )
                return null;
            var hasRole = await _userManager.IsInRoleAsync(user,role);
            if ( !hasRole )
                return null;
            var result = await _userManager.RemoveFromRoleAsync(user,role);
            if ( !result.Succeeded )
                throw new BadRequestException(result.Errors.Select(e => e.Description));
            return $"{role} successfully removed from: {email}";
        }
    }
}
