using System;
using System.Collections.Generic;

namespace Core.Application.DTOs
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }

    public class AuthResponseDto
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public List<string> Errors { get; set; }
    }

    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
