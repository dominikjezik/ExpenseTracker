using ExpenseTracker.Business.Incomes.DTOs;
using ExpenseTracker.Business.Incomes.Queries;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Incomes.Handlers;

public class GetIncomeItemByIdQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetIncomeItemByIdQuery, IncomeDTO?>
{
    public async Task<IncomeDTO?> Handle(GetIncomeItemByIdQuery request, CancellationToken cancellationToken)
    {
        var income = await context.Incomes
            .Where(e => e.Id == request.IncomeId)
            .Include(e => e.Category)
            .FirstOrDefaultAsync();

        if (income == null)
        {
            return null;
        }
        
        if (income.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to view this income.");
        }

        return income.ToDTO();
    }
}
