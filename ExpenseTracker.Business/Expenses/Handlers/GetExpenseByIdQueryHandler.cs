using ExpenseTracker.Business.Expenses.Queries;
using ExpenseTracker.Data.DbContext;
using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Expenses.Handlers;

public class GetExpenseByIdQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetExpenseByIdQuery, Expense?>
{
    public async Task<Expense?> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.Expenses
            .Include(e => e.Tags)
            .FirstOrDefaultAsync(e => e.Id == request.ExpenseId);
    }
}
