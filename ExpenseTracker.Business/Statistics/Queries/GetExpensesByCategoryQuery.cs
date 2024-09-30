using ExpenseTracker.Business.Statistics.DTOs;
using MediatR;

namespace ExpenseTracker.Business.Statistics.Queries;

public record GetExpensesByCategoryQuery(Guid UserId, DateTime? From, DateTime? To) : IRequest<IEnumerable<CategoryExpenseDataItemDTO>>;
