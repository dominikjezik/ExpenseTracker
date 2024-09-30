using ExpenseTracker.Business.Statistics.DTOs;
using MediatR;

namespace ExpenseTracker.Business.Statistics.Queries;

public record GetYearStatisticsQuery(Guid UserId) : IRequest<IEnumerable<MonthDataItemDTO>>;
