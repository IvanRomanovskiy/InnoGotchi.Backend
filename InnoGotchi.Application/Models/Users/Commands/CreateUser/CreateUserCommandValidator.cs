using FluentValidation;

namespace InnoGotchi.Application.Models.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(createCommand => createCommand.FirstName).NotEmpty().MaximumLength(30);
            RuleFor(createCommand => createCommand.LastName).NotEmpty().MaximumLength(30);
            RuleFor(createCommand => createCommand.Email).NotEmpty().EmailAddress();
            RuleFor(createCommand => createCommand.Password).NotEmpty().MinimumLength(8);
            RuleFor(createCommand => createCommand.Avatar).NotEmpty();
        }
    }
}
