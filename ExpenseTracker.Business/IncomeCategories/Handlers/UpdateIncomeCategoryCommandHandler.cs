using ExpenseTracker.Business.IncomeCategories.Commands;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.IncomeCategories.Handlers;

public class UpdateIncomeCategoryCommandHandler(ApplicationDbContext context)
    : IRequestHandler<UpdateIncomeCategoryCommand>
{
    public async Task Handle(UpdateIncomeCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request.ExistingCategory.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this income category.");
        }
        
        if (request.ExistingCategory.Name != request.CategoryForm.Name)
        {
            var existingCategory = await context.IncomeCategories
                .FirstOrDefaultAsync(c => c.UserId == request.UserId && c.Name == request.CategoryForm.Name);
            
            if (existingCategory != null)
            {
                throw new InvalidOperationException($"Category \"{request.CategoryForm.Name}\" already exists.");
            }
        }

        var category = request.ExistingCategory;
        
        category.Name = request.CategoryForm.Name;
        category.Description = request.CategoryForm.Description;
        
        context.IncomeCategories.Update(category);
        
        await context.SaveChangesAsync();
    }
}
