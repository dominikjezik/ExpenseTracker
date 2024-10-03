using ExpenseTracker.Business.ExpenseTemplates.Commands;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTemplates.Handlers;

public class DeleteExpenseTemplateCommandHandler(ApplicationDbContext context)
    : IRequestHandler<DeleteExpenseTemplateCommand>
{
    public async Task Handle(DeleteExpenseTemplateCommand request, CancellationToken cancellationToken)
    {
        if (request.ExpenseTemplate.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this expense template.");
        }

        // Transaction is needed because we deleted cascading tags (error with cycles or multiple cascade paths) 
        var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            context.ExpenseTemplateExpenseTags.RemoveRange(request.ExpenseTemplate.Tags);
            context.ExpenseTemplates.Remove(request.ExpenseTemplate);

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
