using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.DTOs;

namespace Core.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<bool> UpdateUserAsync(UpdateUserDto updateDto);
    }
}
