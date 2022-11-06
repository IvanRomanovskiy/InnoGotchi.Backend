using FluentValidation;

namespace InnoGotchi.Application.Models.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(changePassword => changePassword.OldPassword).MinimumLength(8);
            RuleFor(changePassword => changePassword.NewPassword).MinimumLength(8);
        }
    }
}
