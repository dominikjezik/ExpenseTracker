using ExpenseTracker.Business.Expenses.Commands;
using ExpenseTracker.Business.Expenses.DTOs;
using ExpenseTracker.Data.DbContext;
using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Expenses.Handlers;

public class UpdateExpenseCommandHandler(ApplicationDbContext context)
    : IRequestHandler<UpdateExpenseCommand>
{
    public async Task Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        if (request.ExistingExpense.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this expense.");
        }
        
        var updatedExpense = request.ExpenseForm.ToExpense(request.ExistingExpense);
        
        // Take only selected tags that belong to the selected category or which are general tags
        var selectedTags = request.ExpenseForm.TagIds;
        var selectedCategory = request.ExpenseForm.CategoryId;
        
        updatedExpense.Tags = await context.ExpenseTags
            .Where(et => (selectedTags.Contains(et.Id) && et.CategoryId == selectedCategory) ||
                         (selectedTags.Contains(et.Id) && et.CategoryId == null))
            .Select(et => new ExpenseExpenseTag { ExpenseTagId = et.Id })
            .ToListAsync();
        
        context.Expenses.Update(updatedExpense);
        
        await context.SaveChangesAsync();
    }
}
