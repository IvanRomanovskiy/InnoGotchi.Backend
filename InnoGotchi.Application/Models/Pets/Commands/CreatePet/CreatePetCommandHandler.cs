using InnoGotchi.Application.Common.Exeptions;
using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Models.Pets.Commands.CreatePet
{
    public class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, Guid>
    {
        private readonly IFarmsDbContext farmDbContext;
        private readonly IPetsDbContext petsDbContext;
        private readonly IPetAppearanceDbContext petAppearanceDbContext;
        private readonly IPetsStatusesDbContext petsStatusesDbContext;

        public CreatePetCommandHandler(IFarmsDbContext farmDbContext, IPetsDbContext petsDbContext,
        IPetAppearanceDbContext petAppearanceDbContext, IPetsStatusesDbContext petsStatusesDbContext)
        {
            this.farmDbContext = farmDbContext;
            this.petsDbContext = petsDbContext;
            this.petAppearanceDbContext = petAppearanceDbContext;
            this.petsStatusesDbContext = petsStatusesDbContext;
        }

        public async Task<Guid> Handle(CreatePetCommand request, CancellationToken cancellationToken)
        {
            var farm = await farmDbContext.Farms
                .FirstOrDefaultAsync(f => f.Owner.Id == request.UserId, cancellationToken);
            if (farm == null) throw new NotFoundException(nameof(farm), request.UserId.ToString());

            var body = await petAppearanceDbContext.Bodies
                .FirstOrDefaultAsync(b => b.Id == request.BodyId, cancellationToken);
            if (body == null) throw new NotFoundException(nameof(body), request.BodyId.ToString());

            var eye = await petAppearanceDbContext.Eyes
                .FirstOrDefaultAsync(e => e.Id == request.EyeId, cancellationToken);
            if (eye == null) throw new NotFoundException(nameof(eye), request.EyeId.ToString());

            var mouth = await petAppearanceDbContext.Mouths
                .FirstOrDefaultAsync(m => m.Id == request.MouthId, cancellationToken);
            if (mouth == null) throw new NotFoundException(nameof(mouth), request.MouthId.ToString());

            var nose = await petAppearanceDbContext.Noses
                .FirstOrDefaultAsync(n => n.Id == request.NoseId, cancellationToken);
            if (nose == null) throw new NotFoundException(nameof(nose), request.NoseId.ToString());

            Guid PetId = Guid.NewGuid();

            PetAppearance petAppearance = new PetAppearance
            {
                Id = PetId,
                Body = body,
                Eye = eye,
                Mouth = mouth,
                Nose = nose
            };
            petAppearanceDbContext.PetAppearances.Add(petAppearance);

            PetStatus status = new PetStatus
            {
                Id = PetId,
                HappinessDayCount = 0,
                LastDrinking = DateTime.Now,
                LastFeeding = DateTime.Now,
                HappinessDayStart = DateTime.Now,
                LastUpdating = DateTime.Now,
                HungerLevel = 50,
                ThirstyLevel = 50,
                FeedingCount = 0,
                ThirstQuenchingCount = 0,
                Age = 0
            };

            petsStatusesDbContext.PetsStatuses.Add(status);

            Pet newPet = new Pet
            {
                Id = PetId,
                Name = request.PetName,
                Appearance = petAppearance,
                DateOfBirth = DateTime.Now,
                IsAlive = true,
                Status = status
            };

            petsDbContext.Pets.Add(newPet);
            farm.Pets.Add(newPet);

            await petAppearanceDbContext.SaveChangesAsync(cancellationToken);
            await petsStatusesDbContext.SaveChangesAsync(cancellationToken);
            await petsStatusesDbContext.SaveChangesAsync(cancellationToken);
            await petsDbContext.SaveChangesAsync(cancellationToken);
            await farmDbContext.SaveChangesAsync(cancellationToken);


            return PetId;
        }
    }
}
