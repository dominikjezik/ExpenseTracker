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
        // Ordering by Category name, then by Tag name, but null Category should be at the end
        // Trick from: https://stackoverflow.com/questions/2814742/how-to-order-by-column-with-null-values-last-in-entity-framework
        
        return await context.ExpenseTags
            .Include(t => t.Category)
            .Where(et => et.UserId == request.UserId).OrderBy(t => t.Category == null)
            .ThenBy(t => t.Category == null ? string.Empty : t.Category.Name)
            .ThenBy(t => t.Name)
            .Select(et => et.ToDTO())
            .ToListAsync();
    }
}
