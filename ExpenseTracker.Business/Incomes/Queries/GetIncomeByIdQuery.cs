using ExpenseTracker.Data.Entities.IncomesAggregate;
using MediatR;

namespace ExpenseTracker.Business.Incomes.Queries;

public record GetIncomeByIdQuery(Guid IncomeId) : IRequest<Income?>;
