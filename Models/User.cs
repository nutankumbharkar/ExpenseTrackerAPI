using System.Collections.Generic;

namespace ExpenseTrackerAPI.Models
{ 
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public List<Expense> Expenses { get; set; } = new List<Expense>();
    }
}