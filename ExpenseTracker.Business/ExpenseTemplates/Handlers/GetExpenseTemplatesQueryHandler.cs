using ExpenseTracker.Business.ExpenseTemplates.DTOs;
using ExpenseTracker.Business.ExpenseTemplates.Queries;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.ExpenseTemplates.Handlers;

public class GetExpenseTemplatesQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetExpenseTemplatesQuery, IEnumerable<ExpenseTemplateDTO>>
{
    public async Task<IEnumerable<ExpenseTemplateDTO>> Handle(GetExpenseTemplatesQuery request, CancellationToken cancellationToken)
    {
        var query = context.ExpenseTemplates
            .Where(e => e.UserId == request.UserId);
        
        if (!string.IsNullOrWhiteSpace(request.OrganizationName))
        {
            query = query.Where(e => e.OrganizationName == request.OrganizationName);
        }
        
        return await query.Include(e => e.Category)
            .Include(e => e.Tags)
                .ThenInclude(eet => eet.ExpenseTag)
            .OrderBy(e => e.OrganizationName)
            .Select(e => e.ToDTO())
            .ToListAsync();
    }
}
