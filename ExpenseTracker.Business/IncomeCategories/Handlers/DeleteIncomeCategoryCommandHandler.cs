using ExpenseTracker.Business.IncomeCategories.Commands;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.IncomeCategories.Handlers;

public class DeleteIncomeCategoryCommandHandler(ApplicationDbContext context)
    : IRequestHandler<DeleteIncomeCategoryCommand>
{
    public async Task Handle(DeleteIncomeCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request.Category.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this income category.");
        }
        
        context.IncomeCategories.Remove(request.Category);
        
        await context.SaveChangesAsync();
    }
}
