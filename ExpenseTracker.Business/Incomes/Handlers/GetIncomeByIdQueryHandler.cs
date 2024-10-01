using ExpenseTracker.Business.Incomes.Queries;
using ExpenseTracker.Data.DbContext;
using ExpenseTracker.Data.Entities.IncomesAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Incomes.Handlers;

public class GetIncomeByIdQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetIncomeByIdQuery, Income?>, IRequest<Income?>
{
    public async Task<Income?> Handle(GetIncomeByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.Incomes
            .FirstOrDefaultAsync(e => e.Id == request.IncomeId);
    }
}
