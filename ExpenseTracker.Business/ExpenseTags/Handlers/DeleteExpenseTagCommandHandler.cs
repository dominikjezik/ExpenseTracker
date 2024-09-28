using ExpenseTracker.Business.ExpenseTags.Commands;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTags.Handlers;

public class DeleteExpenseTagCommandHandler(ApplicationDbContext context)
    : IRequestHandler<DeleteExpenseTagCommand>
{
    public async Task Handle(DeleteExpenseTagCommand request, CancellationToken cancellationToken)
    {
        if (request.Tag.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this expense tag.");
        }
        
        context.ExpenseTags.Remove(request.Tag);
        
        await context.SaveChangesAsync();
    }
}
