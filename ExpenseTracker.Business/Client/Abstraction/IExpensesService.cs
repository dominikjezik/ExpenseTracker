using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Expenses.DTOs;

namespace ExpenseTracker.Business.Client.Abstraction;

public interface IExpensesService
{
    Task<Result<List<ExpenseDTO>>> GetExpenses(DateTime fromDate, DateTime toDate);
    
    Task<Result<ExpenseDTO>> GetExpenseById(Guid expenseId);
    
    Task<Result<ExpenseDTO>> CreateExpense(ExpenseFormDTO expenseForm);

    Task<Result<object>> UpdateExpense(Guid expenseId, ExpenseFormDTO expenseForm);
    
    Task<Result<object>> DeleteExpense(Guid expenseId);
}
