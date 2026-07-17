using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsername(string username);
        Task<User?> GetById(int id);
        Task<List<User>> GetAll();
        Task Add(User user);
        Task Update(User user);
        Task Delete(User user);
    }
}
