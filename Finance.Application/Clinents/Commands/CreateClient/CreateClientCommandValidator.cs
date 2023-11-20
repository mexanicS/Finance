using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Clinents.Commands.CreateClient
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator() 
        {
            RuleFor(createClientCommand => createClientCommand.FirstName)
                .NotEmpty()
                .MaximumLength(80);

            RuleFor(createClientCommand => createClientCommand.LastName)
                .NotEmpty()
                .MaximumLength(80);

            RuleFor(createClientCommand => createClientCommand.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
