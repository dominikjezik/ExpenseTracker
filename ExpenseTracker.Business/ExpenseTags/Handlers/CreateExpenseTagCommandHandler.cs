using ExpenseTracker.Business.ExpenseTags.Commands;
using ExpenseTracker.Business.ExpenseTags.DTOs;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.ExpenseTags.Handlers;

public class CreateExpenseTagCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateExpenseTagCommand, ExpenseTagDTO>
{
    public async Task<ExpenseTagDTO> Handle(CreateExpenseTagCommand request, CancellationToken cancellationToken)
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
        
        var tag = request.TagForm.ToExpenseTag();
        tag.UserId = request.UserId;
        
        context.ExpenseTags.Add(tag);
        await context.SaveChangesAsync();
        
        return tag.ToDTO();
    }
}
