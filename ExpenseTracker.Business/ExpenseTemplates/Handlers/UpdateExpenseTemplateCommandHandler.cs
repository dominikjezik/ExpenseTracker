using ExpenseTracker.Business.ExpenseTemplates.Commands;
using ExpenseTracker.Business.ExpenseTemplates.DTOs;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTemplates.Handlers;

public class UpdateExpenseTemplateCommandHandler(ApplicationDbContext context)
    : IRequestHandler<UpdateExpenseTemplateCommand>
{
    public async Task Handle(UpdateExpenseTemplateCommand request, CancellationToken cancellationToken)
    {
        if (request.ExistingExpenseTemplate.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this expense template.");
        }
        
        var updatedExpenseTemplate = request.ExpenseTemplateForm.ToExpenseTemplate(request.ExistingExpenseTemplate);
        
        context.ExpenseTemplates.Update(updatedExpenseTemplate);
        
        await context.SaveChangesAsync();
    }
}
