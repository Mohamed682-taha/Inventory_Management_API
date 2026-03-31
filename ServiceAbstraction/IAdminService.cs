using Shared;

namespace ServiceAbstraction
{
    public interface IAdminService
    {
        Task<string?> AssignRoleAsync(string email,string role);
        Task<string?> ChangeRoleAsync(string email,string role);
        Task<string?> RemoveRoleAsync(string email,string role);
        Task<List<UserWithRoleDto>> GetUsersWithRolesAsync();
    }
}
