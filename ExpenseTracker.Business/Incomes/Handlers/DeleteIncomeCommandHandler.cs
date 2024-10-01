using ExpenseTracker.Business.Incomes.Commands;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.Incomes.Handlers;

public class DeleteIncomeCommandHandler(ApplicationDbContext context)
    : IRequestHandler<DeleteIncomeCommand>
{
    public async Task Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
    {
        if (request.Income.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this income.");
        }
        
        context.Incomes.Remove(request.Income);
        await context.SaveChangesAsync();
    }
}
