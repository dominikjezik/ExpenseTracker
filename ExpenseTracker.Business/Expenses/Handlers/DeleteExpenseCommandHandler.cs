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

        // Transaction is needed because we deleted cascading tags (error with cycles or multiple cascade paths) 
        var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            context.ExpenseExpenseTags.RemoveRange(request.Expense.Tags);
            context.Expenses.Remove(request.Expense);

            await context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
