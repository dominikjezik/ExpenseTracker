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
        return await context.Incomes
            .Where(e => e.UserId == request.UserId)
            .Include(e => e.Category)
            .OrderByDescending(e => e.CreatedAt)
            .Skip(request.Limit * (request.Page - 1))
            .Take(request.Limit)
            .Select(e => e.ToDTO())
            .ToListAsync();
    }
}
