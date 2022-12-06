using FluentValidation;

namespace InnoGotchi.Application.Models.Pets.Commands.CreatePet
{
    public class CreatePetCommandValidator : AbstractValidator<CreatePetCommand>
    {
        public CreatePetCommandValidator()
        {
            RuleFor(createCommand => createCommand.PetName).NotEmpty().MaximumLength(30);
            RuleFor(createCommand => createCommand.EyePath).NotEmpty();
            RuleFor(createCommand => createCommand.NosePath).NotEmpty();
            RuleFor(createCommand => createCommand.BodyPath).NotEmpty();
            RuleFor(createCommand => createCommand.MouthPath).NotEmpty();
        }
    }
}
