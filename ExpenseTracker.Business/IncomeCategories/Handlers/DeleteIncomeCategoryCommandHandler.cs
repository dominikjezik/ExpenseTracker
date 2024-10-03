using ExpenseTracker.Business.IncomeCategories.Commands;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.IncomeCategories.Handlers;

public class DeleteIncomeCategoryCommandHandler(ApplicationDbContext context)
    : IRequestHandler<DeleteIncomeCategoryCommand>
{
    public async Task Handle(DeleteIncomeCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request.Category.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this income category.");
        }
        
        var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            // Update all expenses with this category to null
            var incomes = context.Incomes.Where(e => e.CategoryId == request.Category.Id);
            
            foreach (var income in incomes)
            {
                income.CategoryId = null;
            }
            
            context.Incomes.UpdateRange(incomes);
            
            // Delete the category
            context.IncomeCategories.Remove(request.Category);
            
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
