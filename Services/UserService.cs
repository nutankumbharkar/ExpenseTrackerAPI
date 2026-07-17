using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Exceptions;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<List<UserResponseDto>> GetAllUsers()
        {
            var users = await _userRepo.GetAll();
            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Username = u.Username
            }).ToList();
        }

        public async Task<UserResponseDto> GetUserById(int id)
        {
            var user = await _userRepo.GetById(id);
            if (user == null)
                throw new NotFoundException($"User with id {id} not found");

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username
            };
        }

        public async Task UpdateUser(int id, int requesterId, UpdateUserDto dto)
        {
            if (id != requesterId)
                throw new UnauthorizedAppException("You can only update your own profile");

            var user = await _userRepo.GetById(id);
            if (user == null)
                throw new NotFoundException($"User with id {id} not found");

            user.Username = dto.Username;

            if (!string.IsNullOrEmpty(dto.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _userRepo.Update(user);
        }

        public async Task DeleteUser(int id, int requesterId)
        {
            if (id != requesterId)
                throw new UnauthorizedAppException("You can only delete your own account");

            var user = await _userRepo.GetById(id);
            if (user == null)
                throw new NotFoundException($"User with id {id} not found");

            await _userRepo.Delete(user);
        }
    }
}
