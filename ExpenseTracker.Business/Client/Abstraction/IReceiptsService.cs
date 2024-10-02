using ExpenseTracker.Business.Client.DTOs;
using ExpenseTracker.Business.Client.Helpers;

namespace ExpenseTracker.Business.Client.Abstraction;

public interface IReceiptsService
{
    Task<Result<ReceiptDTO>> GetReceiptByCode(string code);
}
