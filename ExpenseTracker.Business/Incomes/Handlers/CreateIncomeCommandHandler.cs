using ExpenseTracker.Business.Incomes.Commands;
using ExpenseTracker.Business.Incomes.DTOs;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.Incomes.Handlers;

public class CreateIncomeCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateIncomeCommand, IncomeDTO>
{
    public async Task<IncomeDTO> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
    {
        var income = request.IncomeForm.ToIncome();
        income.UserId = request.UserId;
        
        context.Incomes.Add(income);
        await context.SaveChangesAsync();
        
        // Load category
        await context.Entry(income)
            .Reference(i => i.Category)
            .LoadAsync();
        
        return income.ToDTO();
    }
}
