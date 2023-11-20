using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Clinents.Queries.GetClientList
{
    public class GetClientListQueryValidator : AbstractValidator<GetClientListQuery>
    {
        public GetClientListQueryValidator() 
        {
            RuleFor(getClientListQuery => getClientListQuery.Id)
                .NotEqual(Guid.Empty);

            RuleFor(getClientListQuery => getClientListQuery.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
