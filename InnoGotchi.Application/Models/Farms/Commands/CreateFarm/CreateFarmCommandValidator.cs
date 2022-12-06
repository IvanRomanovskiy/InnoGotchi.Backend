using FluentValidation;

namespace InnoGotchi.Application.Farms.Commands.CreateFarm
{
    public class CreateFarmCommandValidator : AbstractValidator<CreateFarmCommand>
    {
        public CreateFarmCommandValidator()
        {
            RuleFor(createFarm => createFarm.Name).NotEmpty();
        }
    }
}
