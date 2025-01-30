using Finance.Application.Clinents.Commands.CreateClient;
using FluentValidation;

namespace Finance.Application.Clinents.Commands.UpdateClient
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
