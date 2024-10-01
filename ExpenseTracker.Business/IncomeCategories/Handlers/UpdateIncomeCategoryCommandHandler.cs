using ExpenseTracker.Business.IncomeCategories.Commands;
using ExpenseTracker.Data.DbContext;
using MediatR;

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

        var category = request.ExistingCategory;
        
        category.Name = request.CategoryForm.Name;
        category.Description = request.CategoryForm.Description;
        
        context.IncomeCategories.Update(category);
        
        await context.SaveChangesAsync();
    }
}
