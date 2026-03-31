using Shared.AccountDto;

namespace ServiceAbstraction
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(RegisterDto dto);
        Task<UserDto?> LoginAsync(LoginDto dto);
    }
}
