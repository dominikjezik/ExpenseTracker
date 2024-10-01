using ExpenseTracker.Business.IncomeCategories.DTOs;
using ExpenseTracker.Data.Entities.IncomesAggregate;
using MediatR;

namespace ExpenseTracker.Business.IncomeCategories.Commands;

public record UpdateIncomeCategoryCommand(IncomeCategoryFormDTO CategoryForm, IncomeCategory ExistingCategory, Guid UserId) : IRequest;
