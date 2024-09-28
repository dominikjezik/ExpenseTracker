using ExpenseTracker.Business.Expenses.Commands;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.Expenses.Handlers;

public class DeleteExpenseCommandHandler(ApplicationDbContext context)
    : IRequestHandler<DeleteExpenseCommand>
{
    public async Task Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        if (request.Expense.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this expense.");
        }
        
        context.Expenses.Remove(request.Expense);
        
        await context.SaveChangesAsync();
    }
}
