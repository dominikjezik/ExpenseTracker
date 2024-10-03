using ExpenseTracker.Business.ExpenseTemplates.Queries;
using ExpenseTracker.Data.DbContext;
using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.ExpenseTemplates.Handlers;

public class GetExpenseTemplateByIdQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetExpenseTemplateByIdQuery, ExpenseTemplate?>
{
    public async Task<ExpenseTemplate?> Handle(GetExpenseTemplateByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.ExpenseTemplates
            .Include(e => e.Tags)
            .FirstOrDefaultAsync(e => e.Id == request.ExpenseTemplateId);
    }
}
