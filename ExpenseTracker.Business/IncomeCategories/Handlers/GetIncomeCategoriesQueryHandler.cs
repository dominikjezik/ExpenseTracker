using ExpenseTracker.Business.IncomeCategories.DTOs;
using ExpenseTracker.Business.IncomeCategories.Queries;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.IncomeCategories.Handlers;

public class GetIncomeCategoriesQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetIncomeCategoriesQuery, IEnumerable<IncomeCategoryDTO>>
{
    public async Task<IEnumerable<IncomeCategoryDTO>> Handle(GetIncomeCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await context.IncomeCategories
            .Where(ec => ec.UserId == request.UserId)
            .OrderBy(ec => ec.Name)
            .Select(ec => ec.ToDTO())
            .ToListAsync();
    }
}
