using FluentValidation;

namespace InnoGotchi.Application.Users.Commands.ChangeName
{
    public class ChangeNameCommandValidator : AbstractValidator<ChangeNameCommand>
    {
        public ChangeNameCommandValidator()
        {
            RuleFor(changeName => changeName.FirstName).MaximumLength(30);
            RuleFor(changeName => changeName.LastName).MaximumLength(30);
        }
    }
}
