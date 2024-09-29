using ExpenseTracker.Business.Expenses.DTOs;
using ExpenseTracker.Business.Expenses.Queries;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Expenses.Handlers;

public class GetExpenseItemByIdQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetExpenseItemByIdQuery, ExpenseDTO?>
{
    public async Task<ExpenseDTO?> Handle(GetExpenseItemByIdQuery request, CancellationToken cancellationToken)
    {
        var expense = await context.Expenses
            .Where(e => e.Id == request.ExpenseId)
            .Include(e => e.Category)
            .Include(e => e.Tags)
                .ThenInclude(eet => eet.ExpenseTag)
            .FirstOrDefaultAsync();

        if (expense == null)
        {
            return null;
        }
        
        if (expense.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to view this expense.");
        }

        return expense.ToDTO();
    }
}
