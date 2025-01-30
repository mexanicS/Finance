using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.FinancialAccountBalance.Commands.DeductMoneyToBalanceByClient
{
    public class DeductMoneyToFinancialAccountCommandValidator : AbstractValidator<DeductMoneyToFinancialAccountCommand>
    {
        public DeductMoneyToFinancialAccountCommandValidator()
        {
            RuleFor(createClientCommand => createClientCommand.Balance)
                .NotNull().WithMessage("Сумма не должна быть пустой")
                .GreaterThanOrEqualTo(0).WithMessage("Сумма должена быть неотрицательным")
                .NotEqual(0).WithMessage("Сумма не должна быть равна нулю");

            RuleFor(createClientCommand => createClientCommand.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
