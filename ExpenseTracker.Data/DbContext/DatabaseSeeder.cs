using Bogus;
using ExpenseTracker.Data.Entities.ExpenseAggregate;
using ExpenseTracker.Data.Entities.IncomesAggregate;
using ExpenseTracker.Data.Identity;

namespace ExpenseTracker.Data.DbContext;

public class DatabaseSeeder(ApplicationDbContext context)
{
    private readonly Faker _faker = new Faker();

    public void Seed(Guid? userId = null)
    {
        var users = new List<ApplicationUser>();

        if (userId.HasValue)
        {
            var user = context.Users.Find(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {userId} not found.");
            }
            
            users.Add(user);
        }
        else
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
        
            users = fakerUsers.Generate(30);
            context.Users.AddRange(users);
        }

        foreach (var user in users)
        {
            GenerateExpensesForUser(user);
            GenerateIncomesForUser(user);
        }
        
        context.SaveChanges();
    }

    private void GenerateExpensesForUser(ApplicationUser user)
    {
        // Categories
        var categoryFood = new ExpenseCategory { Id = Guid.NewGuid(), UserId = user.Id, Name = "Food", Description = _faker.Lorem.Sentence() };
        var categoryTransport = new ExpenseCategory { Id = Guid.NewGuid(), UserId = user.Id, Name = "Transport", Description = _faker.Lorem.Sentence() };
        var categoryEntertainment = new ExpenseCategory { Id = Guid.NewGuid(), UserId = user.Id, Name = "Entertainment", Description = _faker.Lorem.Sentence() };
        var categoryHealth = new ExpenseCategory { Id = Guid.NewGuid(), UserId = user.Id, Name = "Health", Description = _faker.Lorem.Sentence() };
        var categoryClothes = new ExpenseCategory { Id = Guid.NewGuid(), UserId = user.Id, Name = "Clothes", Description = _faker.Lorem.Sentence() };
        var categoryOther = new ExpenseCategory { Id = Guid.NewGuid(), UserId = user.Id, Name = "Other", Description = _faker.Lorem.Sentence() };
        
        var expenseCategories = new List<ExpenseCategory>
        {
            categoryFood,
            categoryTransport,
            categoryEntertainment,
            categoryHealth,
            categoryClothes,
            categoryOther
        };
        
        context.ExpenseCategories.AddRange(expenseCategories);
        
        
        // Expense Tags
        
        var tagsFood = new List<ExpenseTag>
        {
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Restaurant", CategoryId = categoryFood.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Fast food", CategoryId = categoryFood.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Groceries", CategoryId = categoryFood.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Other", CategoryId = categoryFood.Id },
        };
        
        var tagsTransport = new List<ExpenseTag>
        {
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Public transport", CategoryId = categoryTransport.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Taxi", CategoryId = categoryTransport.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Car", CategoryId = categoryTransport.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Other", CategoryId = categoryTransport.Id },
        };
        
        var tagsEntertainment = new List<ExpenseTag>
        {
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Cinema", CategoryId = categoryEntertainment.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Theatre", CategoryId = categoryEntertainment.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Concert", CategoryId = categoryEntertainment.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Other", CategoryId = categoryEntertainment.Id },
        };
        
        var tagsHealth = new List<ExpenseTag>
        {
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Medicine", CategoryId = categoryHealth.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Doctor", CategoryId = categoryHealth.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Pharmacy", CategoryId = categoryHealth.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Other", CategoryId = categoryHealth.Id },
        };
        
        var tagsClothes = new List<ExpenseTag>
        {
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Shirt", CategoryId = categoryClothes.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Trousers", CategoryId = categoryClothes.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Shoes", CategoryId = categoryClothes.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Hat", CategoryId = categoryClothes.Id },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Other", CategoryId = categoryClothes.Id },
        };
        
        var tagsGeneral = new List<ExpenseTag>
        {
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Lorem" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Ipsum" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Dolor" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Sit" },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Amet" }
        };
        
        context.ExpenseTags.AddRange(tagsFood);
        context.ExpenseTags.AddRange(tagsTransport);
        context.ExpenseTags.AddRange(tagsEntertainment);
        context.ExpenseTags.AddRange(tagsHealth);
        context.ExpenseTags.AddRange(tagsClothes);
        context.ExpenseTags.AddRange(tagsGeneral);
        
        
        // Expenses
        var expenses = new List<Expense>();
        var numberOfExpenses = _faker.Random.Int(1, 50);
        
        for (int i = 0; i < numberOfExpenses; i++)
        {
            var category = expenseCategories[_faker.Random.Int(0, expenseCategories.Count - 1)];
            var numberOfGeneralTags = _faker.Random.Int(0, 5);
            var numberOfTargetTags = _faker.Random.Int(0, 4);
            
            var tags = tagsGeneral.OrderBy(t => _faker.Random.Int()).Take(numberOfGeneralTags).ToList();
            tags.AddRange(category.Name switch 
            {
                "Food" => tagsFood.OrderBy(t => _faker.Random.Int()).Take(numberOfTargetTags).ToList(),
                "Transport" => tagsTransport.OrderBy(t => _faker.Random.Int()).Take(numberOfTargetTags).ToList(),
                "Entertainment" => tagsEntertainment.OrderBy(t => _faker.Random.Int()).Take(numberOfTargetTags).ToList(),
                "Health" => tagsHealth.OrderBy(t => _faker.Random.Int()).Take(numberOfTargetTags).ToList(),
                "Clothes" => tagsClothes.OrderBy(t => _faker.Random.Int()).Take(numberOfTargetTags).ToList(),
                _ => new List<ExpenseTag>()
            });
            
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
        
        // Expense Templates
        var expenseTemplates = new List<ExpenseTemplate>();
        var numberOfTemplates = _faker.Random.Int(1, 10);
        
        for (int i = 0; i < numberOfTemplates; i++)
        {
            var category = expenseCategories[_faker.Random.Int(0, expenseCategories.Count - 1)];
            var numberOfGeneralTags = _faker.Random.Int(0, 5);
            var numberOfTargetTags = _faker.Random.Int(0, 4);
            
            var tags = tagsGeneral.OrderBy(t => _faker.Random.Int()).Take(numberOfGeneralTags).ToList();
            tags.AddRange(category.Name switch 
            {
                "Food" => tagsFood.OrderBy(t => _faker.Random.Int()).Take(numberOfTargetTags).ToList(),
                "Transport" => tagsTransport.OrderBy(t => _faker.Random.Int()).Take(numberOfTargetTags).ToList(),
                "Entertainment" => tagsEntertainment.OrderBy(t => _faker.Random.Int()).Take(numberOfTargetTags).ToList(),
                "Health" => tagsHealth.OrderBy(t => _faker.Random.Int()).Take(numberOfTargetTags).ToList(),
                "Clothes" => tagsClothes.OrderBy(t => _faker.Random.Int()).Take(numberOfTargetTags).ToList(),
                _ => new List<ExpenseTag>()
            });
            
            var template = new ExpenseTemplate
            {
                UserId = user.Id,
                OrganizationName = _faker.Company.CompanyName(),
                CategoryId = category.Id,
                Tags = tags.Select(t => new ExpenseTemplateExpenseTag { ExpenseTagId = t.Id }).ToList()
            };
            
            expenseTemplates.Add(template);
        }
        
        context.ExpenseTemplates.AddRange(expenseTemplates);
    }
    
    private void GenerateIncomesForUser(ApplicationUser user)
    {
        // Categories
        var incomeCategories = new List<IncomeCategory>
        {
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Salary", Description = _faker.Lorem.Sentence() },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Freelance", Description = _faker.Lorem.Sentence() },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Investments", Description = _faker.Lorem.Sentence() },
            new() { Id = Guid.NewGuid(), UserId = user.Id, Name = "Other", Description = _faker.Lorem.Sentence() }
        };
        
        context.IncomeCategories.AddRange(incomeCategories);
        
        // Incomes
        var incomes = new List<Income>();
        var numberOfIncomes = _faker.Random.Int(1, 50);
        
        for (int i = 0; i < numberOfIncomes; i++)
        {
            var category = incomeCategories[_faker.Random.Int(0, incomeCategories.Count - 1)];
            
            var income = new Income
            {
                UserId = user.Id,
                CategoryId = category.Id,
                CreatedAt = _faker.Date.Past(),
                Amount = _faker.Random.Decimal(1, 150),
                Description = _faker.Lorem.Sentence()
            };
            
            incomes.Add(income);
        }
        
        context.Incomes.AddRange(incomes);
    }
}
