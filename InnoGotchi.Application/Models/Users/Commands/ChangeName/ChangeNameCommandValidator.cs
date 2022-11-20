using FluentValidation;

namespace InnoGotchi.Application.Models.Users.Commands.ChangeName
{
    public class ChangeNameCommandValidator : AbstractValidator<ChangeNameCommand>
    {
        public ChangeNameCommandValidator()
        {
            RuleFor(changeName => changeName.FirstName).MaximumLength(30);
            RuleFor(changeName => changeName.FirstName).NotEmpty();
            RuleFor(changeName => changeName.LastName).MaximumLength(30);
            RuleFor(changeName => changeName.LastName).NotEmpty();
        }
    }
}
