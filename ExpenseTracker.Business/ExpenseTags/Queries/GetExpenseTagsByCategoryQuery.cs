using ExpenseTracker.Business.ExpenseTags.DTOs;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTags.Queries;

public record GetExpenseTagsByCategoryQuery(Guid UserId, Guid? CategoryId, bool IncludeGeneral) : IRequest<IEnumerable<ExpenseTagDTO>>;
