using ExpenseTracker.Business.ExpenseTemplates.Commands;
using ExpenseTracker.Business.ExpenseTemplates.DTOs;
using ExpenseTracker.Data.DbContext;
using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.ExpenseTemplates.Handlers;

public class CreateExpenseTemplateCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateExpenseTemplateCommand, ExpenseTemplateDTO>
{
    public async Task<ExpenseTemplateDTO> Handle(CreateExpenseTemplateCommand request, CancellationToken cancellationToken)
    {
        var expenseTemplate = request.ExpenseTemplateForm.ToExpenseTemplate();
        expenseTemplate.UserId = request.UserId;
        
        // Take only selected tags that belong to the selected category or which are general tags
        var selectedTags = request.ExpenseTemplateForm.TagIds;
        var selectedCategory = request.ExpenseTemplateForm.CategoryId;
        
        expenseTemplate.Tags = await context.ExpenseTags
            .Where(et => (selectedTags.Contains(et.Id) && et.CategoryId == selectedCategory) ||
                         (selectedTags.Contains(et.Id) && et.CategoryId == null))
            .Select(et => new ExpenseTemplateExpenseTag { ExpenseTagId = et.Id })
            .ToListAsync();
        
        context.ExpenseTemplates.Add(expenseTemplate);
        await context.SaveChangesAsync();
        
        // Load category
        await context.Entry(expenseTemplate)
            .Reference(e => e.Category)
            .LoadAsync();
        
        // Load ExpenseTemplateExpenseTags and nested ExpenseTag
        await context.Entry(expenseTemplate)
            .Collection(e => e.Tags)
            .Query()
            .Include(et => et.ExpenseTag)
            .LoadAsync();

        return expenseTemplate.ToDTO();
    }
}
