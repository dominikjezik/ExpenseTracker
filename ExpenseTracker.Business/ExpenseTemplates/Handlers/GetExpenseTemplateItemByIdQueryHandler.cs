using ExpenseTracker.Business.ExpenseTemplates.DTOs;
using ExpenseTracker.Business.ExpenseTemplates.Queries;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.ExpenseTemplates.Handlers;

public class GetExpenseTemplateItemByIdQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetExpenseTemplateItemByIdQuery, ExpenseTemplateDTO?>
{
    public async Task<ExpenseTemplateDTO?> Handle(GetExpenseTemplateItemByIdQuery request, CancellationToken cancellationToken)
    {
        var expenseTemplate = await context.ExpenseTemplates
            .Where(e => e.Id == request.ExpenseTemplateId)
            .Include(e => e.Category)
            .Include(e => e.Tags)
                .ThenInclude(eet => eet.ExpenseTag)
            .FirstOrDefaultAsync();

        if (expenseTemplate == null)
        {
            return null;
        }
        
        if (expenseTemplate.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to view this expense template.");
        }

        return expenseTemplate.ToDTO();
    }
}
