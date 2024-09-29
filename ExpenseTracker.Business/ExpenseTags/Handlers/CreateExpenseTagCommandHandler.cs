using ExpenseTracker.Business.ExpenseTags.Commands;
using ExpenseTracker.Business.ExpenseTags.DTOs;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTags.Handlers;

public class CreateExpenseTagCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateExpenseTagCommand, ExpenseTagDTO>
{
    public async Task<ExpenseTagDTO> Handle(CreateExpenseTagCommand request, CancellationToken cancellationToken)
    {
        var tag = request.TagForm.ToExpenseTag();
        tag.UserId = request.UserId;
        
        context.ExpenseTags.Add(tag);
        await context.SaveChangesAsync();
        
        return tag.ToDTO();
    }
}
