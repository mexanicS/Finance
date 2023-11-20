using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Clinents.Queries.GetClientDetails
{
    public class GetClientDetailsQueryValidator : AbstractValidator<GetClientDetailsQuery>
    {
        public GetClientDetailsQueryValidator() 
        {
            RuleFor(getClientDetailsQuery => getClientDetailsQuery.Id)
                .NotEqual(Guid.Empty);

            RuleFor(getClientDetailsQuery => getClientDetailsQuery.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
