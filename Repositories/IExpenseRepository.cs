using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Repositories
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> GetAll(int userId);
        Task<Expense> GetById(int id, int userId);
        Task Add(Expense expense);
        Task Update(Expense expense);
        Task Delete(Expense expense);
    }
}
