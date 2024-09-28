using ExpenseTracker.Business.Expenses.DTOs;
using ExpenseTracker.Business.Expenses.Queries;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Expenses.Handlers;

public class GetExpensesQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetExpensesQuery, IEnumerable<ExpenseDTO>>
{
    public async Task<IEnumerable<ExpenseDTO>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
    {
        return await context.Expenses
            .Where(e => e.UserId == request.UserId)
            .Include(e => e.Category)
            .Include(e => e.Tags)
                .ThenInclude(eet => eet.ExpenseTag)
            .OrderByDescending(e => e.CreatedAt)
            .Skip(request.Limit * (request.Page - 1))
            .Take(request.Limit)
            .Select(e => e.ToDTO())
            .ToListAsync();
    }
}
