using ExpenseTracker.Business.ExpenseCategories.Queries;
using ExpenseTracker.Data.DbContext;
using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.ExpenseCategories.Handlers;

public class GetExpenseCategoryByIdQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetExpenseCategoryByIdQuery, ExpenseCategory?>
{
    public async Task<ExpenseCategory?> Handle(GetExpenseCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.ExpenseCategories
            .FirstOrDefaultAsync(e => e.Id == request.CategoryId);
    }
}
