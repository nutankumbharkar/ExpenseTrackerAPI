using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repo;

        public ExpenseService(IExpenseRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Expense>> GetExpenses(int userId)
        {
            return await _repo.GetAll(userId);
        }

        public async Task AddExpense(int userId, ExpenseDto dto)
        {
            var expense = new Expense
            {
                Title = dto.Title,
                Amount = dto.Amount,
                UserId = userId
            };

            await _repo.Add(expense);
        }

        public async Task UpdateExpense(int id, int userId, ExpenseDto dto)
        {
            var expense = await _repo.GetById(id, userId);
            if (expense == null) throw new Exception("Expense not found");

            expense.Title = dto.Title;
            expense.Amount = dto.Amount;

            await _repo.Update(expense);
        }

        public async Task DeleteExpense(int id, int userId)
        {
            var expense = await _repo.GetById(id, userId);
            if (expense == null) throw new Exception("Expense not found");

            await _repo.Delete(expense);
        }
    }
}