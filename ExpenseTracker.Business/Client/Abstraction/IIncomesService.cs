using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Incomes.DTOs;

namespace ExpenseTracker.Business.Client.Abstraction;

public interface IIncomesService
{
    Task<Result<List<IncomeDTO>>> GetIncomes();
    
    Task<Result<IncomeDTO>> GetIncomeById(Guid incomeId);
    
    Task<Result<IncomeDTO>> CreateIncome(IncomeFormDTO incomeForm);

    Task<Result<object>> UpdateIncome(Guid incomeId, IncomeFormDTO incomeForm);
    
    Task<Result<object>> DeleteIncome(Guid incomeId);
}
