using ExpenseTracker.Business.Statistics.DTOs;
using MediatR;

namespace ExpenseTracker.Business.Statistics.Queries;

public record GetBalanceQuery(Guid UserId, DateTime? From, DateTime? To) : IRequest<BalanceDTO>;
