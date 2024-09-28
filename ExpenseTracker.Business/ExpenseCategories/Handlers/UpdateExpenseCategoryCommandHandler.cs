using ExpenseTracker.Business.ExpenseCategories.Commands;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.ExpenseCategories.Handlers;

public class UpdateExpenseCategoryCommandHandler(ApplicationDbContext context)
    : IRequestHandler<UpdateExpenseCategoryCommand>
{
    public async Task Handle(UpdateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request.ExistingCategory.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this expense category.");
        }

        var category = request.ExistingCategory;
        
        category.Name = request.CategoryForm.Name;
        category.Description = request.CategoryForm.Description;
        
        context.ExpenseCategories.Update(category);
        
        await context.SaveChangesAsync();
    }
}
