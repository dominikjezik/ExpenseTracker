using ExpenseTracker.Business.ExpenseCategories.Commands;
using ExpenseTracker.Business.ExpenseCategories.DTOs;
using ExpenseTracker.Business.Expenses.DTOs;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.ExpenseCategories.Handlers;

public class CreateExpenseCategoryCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateExpenseCategoryCommand, ExpenseCategoryDTO>
{
    public async Task<ExpenseCategoryDTO> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = request.CategoryForm.ToExpenseCategory();
        category.UserId = request.UserId;

        context.ExpenseCategories.Add(category);
        await context.SaveChangesAsync();

        return category.ToDTO();
    }
}
