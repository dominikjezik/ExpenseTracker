using ExpenseTracker.Business.IncomeCategories.Commands;
using ExpenseTracker.Business.IncomeCategories.DTOs;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.IncomeCategories.Handlers;

public class CreateIncomeCategoryCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateIncomeCategoryCommand, IncomeCategoryDTO>
{
    public async Task<IncomeCategoryDTO> Handle(CreateIncomeCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = request.CategoryForm.ToIncomeCategory();
        category.UserId = request.UserId;

        context.IncomeCategories.Add(category);
        await context.SaveChangesAsync();

        return category.ToDTO();
    }
}
