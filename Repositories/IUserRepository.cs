using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsername(string username);
        Task<User?> GetById(int id);
        Task Add(User user);
    }
}
