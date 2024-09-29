using ExpenseTracker.Business.ExpenseCategories.DTOs;
using ExpenseTracker.Business.ExpenseCategories.Queries;
using ExpenseTracker.Business.Expenses.DTOs;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.ExpenseCategories.Handlers;

public class GetExpenseCategoriesQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetExpenseCategoriesQuery, IEnumerable<ExpenseCategoryDTO>>
{
    public async Task<IEnumerable<ExpenseCategoryDTO>> Handle(GetExpenseCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await context.ExpenseCategories
            .Where(ec => ec.UserId == request.UserId)
            .OrderBy(ec => ec.Name)
            .Select(ec => ec.ToDTO())
            .ToListAsync();
    }
}
