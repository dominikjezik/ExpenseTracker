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
            var tagExists = await context.ExpenseTags
                .AnyAsync(t => t.Name == request.TagForm.Name 
                               && t.UserId == request.UserId
                               && t.CategoryId == request.TagForm.CategoryId);

            if (tagExists)
            {
                if (request.TagForm.CategoryId == null)
                {
                    throw new InvalidOperationException($"Tag \"{request.TagForm.Name}\" already exists.");
                }

                throw new InvalidOperationException($"Tag \"{request.TagForm.Name}\" already exists for selected category.");
            }
        }
        
        request.ExistingTag.Name = request.TagForm.Name;
        request.ExistingTag.CategoryId = request.TagForm.CategoryId;
        
        context.ExpenseTags.Update(request.ExistingTag);
        
        await context.SaveChangesAsync();
    }
}
