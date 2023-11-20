using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Clinents.Commands.DeleteClient
{
    public class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
    {
        public DeleteClientCommandValidator() 
        {
            RuleFor(createClientCommand => createClientCommand.Id)
                .NotEqual(Guid.Empty);

            RuleFor(createClientCommand => createClientCommand.UserId)
                .NotEqual(Guid.Empty);

        }
    }
}
