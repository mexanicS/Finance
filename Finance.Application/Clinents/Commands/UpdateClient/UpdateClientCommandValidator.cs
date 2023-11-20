using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Clinents.Commands.CreateClient
{
    public class UpdateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            RuleFor(createClientCommand => createClientCommand.UserId)
                .NotEqual(Guid.Empty);

            RuleFor(createClientCommand => createClientCommand.Id)
                .NotEqual(Guid.Empty);

            RuleFor(createClientCommand => createClientCommand.FirstName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(createClientCommand => createClientCommand.LastName)
                .NotEmpty()
                .MaximumLength(150);
        }
    }
}
