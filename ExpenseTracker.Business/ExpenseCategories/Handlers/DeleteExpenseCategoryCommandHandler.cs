using ExpenseTracker.Business.ExpenseCategories.Commands;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.ExpenseCategories.Handlers;

public class DeleteExpenseCategoryCommandHandler(ApplicationDbContext context)
    : IRequestHandler<DeleteExpenseCategoryCommand>
{
    public async Task Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request.Category.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this expense category.");
        }
        
        var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            // Update all expenses with this category to null
            var expenses = context.Expenses.Where(e => e.CategoryId == request.Category.Id);
            foreach (var expense in expenses)
            {
                expense.CategoryId = null;
            }
            
            context.Expenses.UpdateRange(expenses);
            
            // Update add expense templates with this category to null
            var expenseTemplates = context.ExpenseTemplates.Where(e => e.CategoryId == request.Category.Id);
            foreach (var expenseTemplate in expenseTemplates)
            {
                expenseTemplate.CategoryId = null;
            }
            
            context.ExpenseTemplates.UpdateRange(expenseTemplates);
            
            // Delete the category
            context.ExpenseCategories.Remove(request.Category);
            
            await context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
