using ExpenseTracker.Data.Entities.ExpenseAggregate;
using ExpenseTracker.Data.Entities.IncomesAggregate;
using ExpenseTracker.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.DbContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Expense>()
            .Property(e => e.Amount)
            .HasColumnType("money");

        builder.Entity<ExpenseExpenseTag>()
            .HasKey(eet => new { eet.ExpenseId, eet.ExpenseTagId });
        
        // Added OnDelete(DeleteBehavior.NoAction) to solve "cycles":
        // Introducing FOREIGN KEY constraint 'FK_ExpenseExpenseTags_Expenses_ExpenseId' on table 'ExpenseExpenseTags'
        // may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify
        // other FOREIGN KEY constraints.
        builder.Entity<Expense>()
            .HasMany(e => e.Tags)
            .WithOne(eet => eet.Expense)
            .HasForeignKey(eet => eet.ExpenseId)
            .OnDelete(DeleteBehavior.NoAction); // Change to NoAction to avoid cycles
    }

    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    public DbSet<ExpenseTag> ExpenseTags { get; set; }
    public DbSet<ExpenseExpenseTag> ExpenseExpenseTags { get; set; }

    public DbSet<Income> Incomes { get; set; }
    public DbSet<IncomeCategory> IncomeCategories { get; set; }
}
