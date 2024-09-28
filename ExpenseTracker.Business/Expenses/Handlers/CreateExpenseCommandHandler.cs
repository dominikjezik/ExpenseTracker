using ExpenseTracker.Business.Expenses.Commands;
using ExpenseTracker.Business.Expenses.DTOs;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Expenses.Handlers;

public class CreateExpenseCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateExpenseCommand, ExpenseDTO>
{
    public async Task<ExpenseDTO> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = request.ExpenseForm.ToExpense();
        expense.UserId = request.UserId;

        context.Expenses.Add(expense);
        await context.SaveChangesAsync();
        
        // Load category
        await context.Entry(expense)
            .Reference(e => e.Category)
            .LoadAsync();
        
        // Load ExpenseExpenseTags and nested ExpenseTag
        await context.Entry(expense)
            .Collection(e => e.Tags)
            .Query()
            .Include(et => et.ExpenseTag)
            .LoadAsync();

        return expense.ToDTO();
    }
}
