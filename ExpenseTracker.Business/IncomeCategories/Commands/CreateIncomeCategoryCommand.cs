using ExpenseTracker.Business.IncomeCategories.DTOs;
using MediatR;

namespace ExpenseTracker.Business.IncomeCategories.Commands;

public record CreateIncomeCategoryCommand(IncomeCategoryFormDTO CategoryForm, Guid UserId) : IRequest<IncomeCategoryDTO>;
