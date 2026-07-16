using Microsoft.EntityFrameworkCore;
using ExpenseTrackerAPI.Models;


namespace ExpenseTrackerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // ✅ Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        // ✅ Model Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Decimal precision fix
            modelBuilder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasPrecision(18, 2);

            // ✅ Foreign Key Relation (User → Expenses)
            modelBuilder.Entity<Expense>()
                .HasOne<User>()              // Each expense belongs to one user
                .WithMany()                 // One user has many expenses
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.User)
                .WithMany(u => u.Expenses)
                .HasForeignKey(e => e.UserId);
        }
    }
}