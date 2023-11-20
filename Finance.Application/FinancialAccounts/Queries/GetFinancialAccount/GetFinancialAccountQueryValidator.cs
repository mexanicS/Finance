using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.FinancialAccounts.Queries.GetFinancialAccount
{
    public class GetFinancialAccountQueryValidator : AbstractValidator<GetFinancialAccountQuery>
    {
        public GetFinancialAccountQueryValidator() 
        {
            RuleFor(getClientDetailsQuery => getClientDetailsQuery.Id)
                .NotEqual(Guid.Empty);

            RuleFor(getClientDetailsQuery => getClientDetailsQuery.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
