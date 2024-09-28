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
        return await context.Expenses
            .Where(e => e.Id == request.ExpenseId/* && e.UserId == request.UserId*/)
            .Include(e => e.Category)
            .Include(e => e.Tags)
                .ThenInclude(eet => eet.ExpenseTag)
            .Select(e => e.ToDTO())
            .FirstOrDefaultAsync();
    }
}
