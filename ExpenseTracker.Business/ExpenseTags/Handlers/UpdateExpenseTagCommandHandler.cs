using ExpenseTracker.Business.ExpenseTags.Commands;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTags.Handlers;

public class UpdateExpenseTagCommandHandler(ApplicationDbContext context)
    : IRequestHandler<UpdateExpenseTagCommand>
{
    public async Task Handle(UpdateExpenseTagCommand request, CancellationToken cancellationToken)
    {
        if (request.ExistingTag.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this expense tag.");
        }
        
        request.ExistingTag.Name = request.TagForm.Name;
        
        context.ExpenseTags.Update(request.ExistingTag);
        
        await context.SaveChangesAsync();
    }
}
