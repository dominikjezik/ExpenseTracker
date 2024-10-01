using ExpenseTracker.Business.IncomeCategories.Queries;
using ExpenseTracker.Data.DbContext;
using ExpenseTracker.Data.Entities.IncomesAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.IncomeCategories.Handlers;

public class GetIncomeCategoryByIdQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetIncomeCategoryByIdQuery, IncomeCategory?>
{
    public async Task<IncomeCategory?> Handle(GetIncomeCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.IncomeCategories
            .FirstOrDefaultAsync(e => e.Id == request.CategoryId);
    }
}
