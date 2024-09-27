using Bogus;
using ExpenseTracker.Data.Entities.ExpenseAggregate;
using ExpenseTracker.Data.Identity;

namespace ExpenseTracker.Data.DbContext;

public class DatabaseSeeder(ApplicationDbContext context)
{
    private readonly Faker _faker = new Faker();

    public void Seed()
    {
        // Password is "Password1"
        var fakerUsers = new Faker<ApplicationUser>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email())
            .RuleFor(u => u.UserName, (f, u) => u.Email)
            .RuleFor(u => u.NormalizedUserName, (f, u) => u.Email?.ToUpper())
            .RuleFor(u => u.NormalizedEmail, (f, u) => u.Email?.ToUpper())
            .RuleFor(u => u.EmailConfirmed, f => f.Random.Bool())
            .RuleFor(u => u.PasswordHash,
                f => "AQAAAAIAAYagAAAAEEBlNmh1Dg1K0iOOd5OpuZWN75eNALqi89MQFEyZC/V6y7j+p0iYOEsQL3EFbF/hcw==")
            .RuleFor(u => u.SecurityStamp, f => Guid.NewGuid().ToString())
            .RuleFor(u => u.ConcurrencyStamp, f => Guid.NewGuid().ToString())
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.PhoneNumberConfirmed, f => f.Random.Bool())
            .RuleFor(u => u.TwoFactorEnabled, f => false)
            .RuleFor(u => u.LockoutEnd, f => null)
            .RuleFor(u => u.LockoutEnabled, f => false)
            .RuleFor(u => u.AccessFailedCount, f => 0);
        
        var users = fakerUsers.Generate(30);
        context.Users.AddRange(users);

        foreach (var user in users)
        {
            GenerateExpensesForUser(user);
        }
        
        context.SaveChanges();
    }

    private void GenerateExpensesForUser(ApplicationUser user)
    {
        // Categories
        var expenseCategories = new List<ExpenseCategory>
        {
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Food" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Transport" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Entertainment" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Health" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Clothes" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Other" }
        };
        
        context.ExpenseCategories.AddRange(expenseCategories);
        
        
        // Tags
        var expenseTags = new List<ExpenseTag>
        {
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Restaurant" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Fast food" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Public transport" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Taxi" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Cinema" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Theatre" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Medicine" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Doctor" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Pharmacy" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Shirt" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Trousers" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Shoes" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Hat" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Other" }
        };
        
        context.ExpenseTags.AddRange(expenseTags);
        
        
        // Expenses
        var expenses = new List<Expense>();
        var numberOfExpenses = _faker.Random.Int(1, 50);
        
        for (int i = 0; i < numberOfExpenses; i++)
        {
            var category = expenseCategories[_faker.Random.Int(0, expenseCategories.Count - 1)];
            var numberOfTags = _faker.Random.Int(0, 5);
            var tags = expenseTags.OrderBy(t => _faker.Random.Int()).Take(numberOfTags).ToList();
            
            var expense = new Expense
            {
                UserId = user.Id,
                CategoryId = category.Id,
                CreatedAt = _faker.Date.Past(),
                Amount = _faker.Random.Decimal(1, 150),
                Description = _faker.Lorem.Sentence(),
                Tags = tags.Select(t => new ExpenseExpenseTag { ExpenseTagId = t.Id }).ToList()
            };
            
            expenses.Add(expense);
        }
        
        context.Expenses.AddRange(expenses);
    }
}
