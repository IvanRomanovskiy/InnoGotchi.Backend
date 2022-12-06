using InnoGotchi.Application.Common.Exeptions;
using InnoGotchi.Application.Common.Extentions;
using InnoGotchi.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Models.Farms.Queries.GetFarmInfo
{
    public class GetFarmInfoQueryHandler : IRequestHandler<GetFarmInfoQuery, FarmInfoVm>
    {
        private readonly IFarmsDbContext farmsDbContext;
        private readonly IPetsDbContext petsDbContext;

        public GetFarmInfoQueryHandler(IFarmsDbContext farmsDbContext,IPetsDbContext petsDbContext)
        {
            this.farmsDbContext = farmsDbContext;
            this.petsDbContext = petsDbContext;
        }

        public async Task<FarmInfoVm> Handle(GetFarmInfoQuery request, CancellationToken cancellationToken)
        {

            var farm = await farmsDbContext.Farms.Include(p => p.Pets)
                .FirstOrDefaultAsync(f => f.Owner.Id == request.UserId, cancellationToken);

            if (farm == null) throw new NotFoundException(nameof(farm), request.UserId.ToString());

            int alivePets;
            int deadPets;
            long averagePetsHappinessDays;
            TimeSpan averageFeedingPeriod;
            TimeSpan averageThirstQuenchingPeriod;
            long averagePetsAgeCount;

            var pets = petsDbContext.Pets
                .Where(pet => farm.Pets
                .Select(farmPet => farmPet.Id)
                .Contains(pet.Id))
                .Include(pet => pet.Status);

            try { alivePets = pets.Where(pet => pet.IsAlive).Count(); }
            catch { alivePets = 0; }

            try { deadPets = pets.Where(pet => !pet.IsAlive).Count(); }
            catch { deadPets = 0; }

            try
            {
                averagePetsHappinessDays =
                pets
                .Select(pet => pet.Status.HappinessDayCount)
                .Sum(status => status) / farm.Pets.Count;
            }
            catch { averagePetsHappinessDays = 0; }



            try
            {
                var ticksFeeding = pets.Select(pet => (pet.Status.FeedingCount != 0) ?
                (((pet.IsAlive ? DateTime.Now : pet.DateOfDeath) ?? DateTime.Now).Ticks - pet.DateOfBirth.Ticks) / pet.Status.FeedingCount : 0);

                averageFeedingPeriod = TimeSpan.FromTicks(ticksFeeding.ToList().Sum() / pets.Count()).StripMilliseconds();
            }
            catch { averageFeedingPeriod = new TimeSpan(); }

            try
            {
                var ticksThirstQuenching = pets.Select(pet => (pet.Status.FeedingCount != 0) ?
                (((pet.IsAlive ? DateTime.Now : pet.DateOfDeath) ?? DateTime.Now).Ticks - pet.DateOfBirth.Ticks) / pet.Status.ThirstQuenchingCount : 0);

                averageThirstQuenchingPeriod = TimeSpan.FromTicks(ticksThirstQuenching.ToList().Sum() / pets.Count()).StripMilliseconds();
            }
            catch { averageThirstQuenchingPeriod = new TimeSpan(); }



            try
            {
                averagePetsAgeCount =
                pets
                .Select(pet => pet.Status)
                .Sum(status => status.Age) / farm.Pets.Count;
            }
            catch { averagePetsAgeCount = 0; }

            FarmInfoVm farmInfoVm = new FarmInfoVm
            {
                Name = farm.Name,
                CountAlivePets = alivePets,
                CountDeadPets = deadPets,
                AveragePetsHappinessDaysCount = averagePetsHappinessDays,
                AverageFeedingPeriod = averageFeedingPeriod,
                AverageThirstQuenchingPeriod =averageThirstQuenchingPeriod,
                AveragePetsAgeCount = averagePetsAgeCount
            };

            return farmInfoVm;
        }
    }
}
