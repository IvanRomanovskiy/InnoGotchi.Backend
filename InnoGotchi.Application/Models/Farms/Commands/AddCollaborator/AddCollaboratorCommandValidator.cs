using FluentValidation;

namespace InnoGotchi.Application.Farms.Commands.AddCollaborator
{
    public class AddCollaboratorCommandValidator : AbstractValidator<AddCollaboratorCommand>
    {
        public AddCollaboratorCommandValidator()
        {
            RuleFor(addCollaborator => addCollaborator.CollaboratorEmail).EmailAddress();
        }
    }
}
