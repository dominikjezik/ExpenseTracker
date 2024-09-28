using ExpenseTracker.Business.ExpenseTags.Queries;
using ExpenseTracker.Data.DbContext;
using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.ExpenseTags.Handlers;

public class GetExpenseTagByIdQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetExpenseTagByIdQuery, ExpenseTag?>
{
    public async Task<ExpenseTag?> Handle(GetExpenseTagByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.ExpenseTags
            .FirstOrDefaultAsync(e => e.Id == request.TagId);
    }
}
