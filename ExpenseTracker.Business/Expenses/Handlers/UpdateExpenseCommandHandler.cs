using ExpenseTracker.Business.Expenses.Commands;
using ExpenseTracker.Business.Expenses.DTOs;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.Expenses.Handlers;

public class UpdateExpenseCommandHandler(ApplicationDbContext context)
    : IRequestHandler<UpdateExpenseCommand>
{
    public async Task Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        if (request.ExistingExpense.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this expense.");
        }
        
        var updatedExpense = request.ExpenseForm.ToExpense(request.ExistingExpense);
        
        context.Expenses.Update(updatedExpense);
        
        await context.SaveChangesAsync();
    }
}
