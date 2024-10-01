using ExpenseTracker.Business.Incomes.Commands;
using ExpenseTracker.Business.Incomes.DTOs;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.Incomes.Handlers;

public class UpdateIncomeCommandHandler(ApplicationDbContext context)
    : IRequestHandler<UpdateIncomeCommand>
{
    public async Task Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
    {
        if (request.ExistingIncome.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this income.");
        }
        
        var updatedIncome = request.IncomeForm.ToIncome(request.ExistingIncome);
        
        context.Incomes.Update(updatedIncome);
        
        await context.SaveChangesAsync();
    }
}