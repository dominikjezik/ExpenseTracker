using ExpenseTracker.Business.IncomeCategories.Commands;
using ExpenseTracker.Business.IncomeCategories.DTOs;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.IncomeCategories.Handlers;

public class CreateIncomeCategoryCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateIncomeCategoryCommand, IncomeCategoryDTO>
{
    public async Task<IncomeCategoryDTO> Handle(CreateIncomeCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await context.IncomeCategories
            .AnyAsync(c => c.Name == request.CategoryForm.Name && c.UserId == request.UserId);
        
        if (categoryExists)
        {
            throw new InvalidOperationException($"Category \"{request.CategoryForm.Name}\" already exists.");
        }
        
        var category = request.CategoryForm.ToIncomeCategory();
        category.UserId = request.UserId;

        context.IncomeCategories.Add(category);
        await context.SaveChangesAsync();

        return category.ToDTO();
    }
}
