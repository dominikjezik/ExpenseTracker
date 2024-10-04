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
        var query = context.Expenses
            .Where(e => e.UserId == request.UserId);
        
        if (request.FromDate.HasValue && request.ToDate.HasValue)
        {
            query = query.Where(e => e.CreatedAt >= request.FromDate && e.CreatedAt < request.ToDate);
        }
        
        return await query
            .Include(e => e.Category)
            .Include(e => e.Tags)
                .ThenInclude(eet => eet.ExpenseTag)
            .OrderByDescending(e => e.CreatedAt)
            .Select(e => e.ToDTO())
            .ToListAsync();
    }
}
