using ExpenseTracker.Business.ExpenseTags.Commands;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        
                
        if (request.ExistingTag.Name != request.TagForm.Name)
        {
            var existingTag = await context.ExpenseTags
                .FirstOrDefaultAsync(c => c.UserId == request.UserId && c.Name == request.TagForm.Name);
            
            if (existingTag != null)
            {
                throw new InvalidOperationException($"Tag \"{request.TagForm.Name}\" already exists.");
            }
        }
        
        request.ExistingTag.Name = request.TagForm.Name;
        
        context.ExpenseTags.Update(request.ExistingTag);
        
        await context.SaveChangesAsync();
    }
}
