using ExpenseTracker.Business.ExpenseCategories.Commands;
using ExpenseTracker.Business.ExpenseCategories.DTOs;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.ExpenseCategories.Handlers;

public class CreateExpenseCategoryCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateExpenseCategoryCommand, ExpenseCategoryDTO>
{
    public async Task<ExpenseCategoryDTO> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await context.ExpenseCategories
            .AnyAsync(c => c.Name == request.CategoryForm.Name && c.UserId == request.UserId);
        
        if (categoryExists)
        {
            throw new InvalidOperationException($"Category \"{request.CategoryForm.Name}\" already exists.");
        }
        
        var category = request.CategoryForm.ToExpenseCategory();
        category.UserId = request.UserId;

        context.ExpenseCategories.Add(category);
        await context.SaveChangesAsync();

        return category.ToDTO();
    }
}
