using ExpenseTracker.Business.ExpenseTags.DTOs;
using ExpenseTracker.Business.ExpenseTags.Queries;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.ExpenseTags.Handlers;

public class GetExpenseTagsQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetExpenseTagsQuery, IEnumerable<ExpenseTagDTO>>
{
    public async Task<IEnumerable<ExpenseTagDTO>> Handle(GetExpenseTagsQuery request, CancellationToken cancellationToken)
    {
        return await context.ExpenseTags
            .Where(et => et.UserId == request.UserId)
            .Select(et => et.ToDTO())
            .ToListAsync();
    }
}
