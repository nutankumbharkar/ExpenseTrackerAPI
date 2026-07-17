using ExpenseTrackerAPI.DTOs;

namespace ExpenseTrackerAPI.Services
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllUsers();
        Task<UserResponseDto> GetUserById(int id);
        Task UpdateUser(int id, int requesterId, UpdateUserDto dto);
        Task DeleteUser(int id, int requesterId);
    }
}
