using ExpenseTracker.Business.ExpenseTemplates.Commands;
using ExpenseTracker.Business.ExpenseTemplates.DTOs;
using ExpenseTracker.Data.DbContext;
using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        
        // Take only selected tags that belong to the selected category or which are general tags
        var selectedTags = request.ExpenseTemplateForm.TagIds;
        var selectedCategory = request.ExpenseTemplateForm.CategoryId;
        
        updatedExpenseTemplate.Tags = await context.ExpenseTags
            .Where(et => (selectedTags.Contains(et.Id) && et.CategoryId == selectedCategory) ||
                         (selectedTags.Contains(et.Id) && et.CategoryId == null))
            .Select(et => new ExpenseTemplateExpenseTag { ExpenseTagId = et.Id })
            .ToListAsync();
        
        context.ExpenseTemplates.Update(updatedExpenseTemplate);
        
        await context.SaveChangesAsync();
    }
}
