using ExpenseTracker.Business.Incomes.DTOs;
using ExpenseTracker.Business.Incomes.Queries;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Incomes.Handlers;

public class GetIncomesQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetIncomesQuery, IEnumerable<IncomeDTO>>
{
    public async Task<IEnumerable<IncomeDTO>> Handle(GetIncomesQuery request, CancellationToken cancellationToken)
    {
        var query = context.Incomes
            .Where(e => e.UserId == request.UserId);

        if (request.FromDate.HasValue && request.ToDate.HasValue)
        {
            query = query.Where(e => e.CreatedAt >= request.FromDate && e.CreatedAt < request.ToDate);
        }
        
        return await query
            .Include(e => e.Category)
            .OrderByDescending(e => e.CreatedAt)
            .Select(e => e.ToDTO())
            .ToListAsync();
    }
}
