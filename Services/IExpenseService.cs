using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services
{
    public interface IExpenseService
    {
        Task<List<Expense>> GetExpenses(int userId);
        Task AddExpense(int userId, ExpenseDto dto);
        Task UpdateExpense(int id, int userId, ExpenseDto dto);
        Task DeleteExpense(int id, int userId);
    }
}
