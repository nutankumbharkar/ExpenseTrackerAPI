using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IUserRepository userRepo, JwtHelper jwtHelper)
        {
            _userRepo = userRepo;
            _jwtHelper = jwtHelper;
        }

        public async Task<string> Register(RegisterDto dto)
        {
            var existing = await _userRepo.GetByUsername(dto.Username);
            if (existing != null)
                throw new Exception("Username already exists");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _userRepo.Add(user);

            return _jwtHelper.GenerateToken(user.Id);
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await _userRepo.GetByUsername(dto.Username);
            if (user == null)
                throw new Exception("Invalid username or password");

            bool valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!valid)
                throw new Exception("Invalid username or password");

            return _jwtHelper.GenerateToken(user.Id);
        }
    }
}
