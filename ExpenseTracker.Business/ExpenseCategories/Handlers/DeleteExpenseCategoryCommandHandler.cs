using ExpenseTracker.Business.ExpenseCategories.Commands;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.ExpenseCategories.Handlers;

public class DeleteExpenseCategoryCommandHandler(ApplicationDbContext context)
    : IRequestHandler<DeleteExpenseCategoryCommand>
{
    public async Task Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request.Category.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this expense category.");
        }
        
        context.ExpenseCategories.Remove(request.Category);
        
        await context.SaveChangesAsync();
    }
}
