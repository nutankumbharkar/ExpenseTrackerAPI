using ExpenseTrackerAPI.DTOs;

namespace ExpenseTrackerAPI.Services
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDto dto);
        Task<string> Login(LoginDto dto);
    }
}
