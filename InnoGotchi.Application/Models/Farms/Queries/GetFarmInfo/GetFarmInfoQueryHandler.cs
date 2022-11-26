using InnoGotchi.Application.Common.Exeptions;
using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Models.Farms.Queries.GetFarmInfo
{
    public class GetFarmInfoQueryHandler : IRequestHandler<GetFarmInfoQuery, FarmInfoVm>
    {
        private readonly IFarmsDbContext farmsDbContext;
        private readonly IPetsStatusesDbContext statusesDbContext;

        public GetFarmInfoQueryHandler(IFarmsDbContext farmsDbContext, IPetsStatusesDbContext statusesDbContext)
        {
            this.farmsDbContext = farmsDbContext;
            this.statusesDbContext = statusesDbContext;
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

            try { alivePets = farm.Pets.Where(pet => pet.IsAlive).Count(); }
            catch { alivePets = 0; }

            try { deadPets = farm.Pets.Where(pet => !pet.IsAlive).Count(); }
            catch { deadPets = 0; }

            var statuses = statusesDbContext.PetsStatuses
                .Where(status => farm.Pets
                .Select(pet => pet.Id)
                .Contains(status.Id));

            farm.Pets.Join(statuses, pet => pet.Id, status => status.Id, (pet,status) => 
            {
                pet.Status = status;
                return pet;
            });

            try
            {
                averagePetsHappinessDays =
                farm.Pets
                .Select(pet => pet.Status.HappinessDayCount)
                .Sum(status => status) / farm.Pets.Count;
            }
            catch { averagePetsHappinessDays = 0; }
            try
            {
                averageFeedingPeriod = new TimeSpan(
                Convert.ToInt64(farm.Pets
                .Sum(
                    pet =>
                    (((pet.IsAlive ? DateTime.Now : pet.DateOfDeath) ?? DateTime.Now)
                    .Subtract(pet.DateOfDeath ?? DateTime.Now) / pet.Status.FeedingCount).TotalHours
                    ) / farm.Pets.Count));
            }
            catch { averageFeedingPeriod = TimeSpan.Zero; }
            try
            {
                averageThirstQuenchingPeriod = new TimeSpan(
                Convert.ToInt64(farm.Pets
                .Sum(
                    pet =>
                    (((pet.IsAlive ? DateTime.Now : pet.DateOfDeath) ?? DateTime.Now)
                    .Subtract(pet.DateOfDeath ?? DateTime.Now) / pet.Status.ThirstQuenchingCount).TotalHours
                    ) / farm.Pets.Count));
            }
            catch { averageThirstQuenchingPeriod = TimeSpan.Zero; }
            try
            {
                averagePetsAgeCount =
                farm.Pets
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
