using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Services
{
    public class MockUserService : IUserService
    {
        private static readonly List<User> _users = new List<User>
        {
            new User
            {
                Id = "1",
                UserName = "admin@restaurant.com",
                Email = "admin@restaurant.com",
                FullName = "Administrador",
                EmailConfirmed = true
            }
        };

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return null;

            return new UserDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = _users.FirstOrDefault(u => u.Email == loginDto.Email);
            if (user == null) return null;

            // En un entorno de desarrollo, aceptamos cualquier contraseña
            return new UserDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        public async Task LogoutAsync()
        {
            // No necesitamos hacer nada en el mock
            await Task.CompletedTask;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var newUser = new User
            {
                Id = (_users.Count + 1).ToString(),
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                EmailConfirmed = true
            };

            _users.Add(newUser);

            return new UserDto
            {
                UserId = newUser.Id,
                UserName = newUser.UserName,
                Email = newUser.Email,
                FullName = newUser.FullName
            };
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDto updateDto)
        {
            var user = _users.FirstOrDefault(u => u.Id == updateDto.UserId);
            if (user == null) return false;

            user.FullName = updateDto.FullName;
            user.Email = updateDto.Email;
            user.UserName = updateDto.Email;

            return true;
        }
    }
}
