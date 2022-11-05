using InnoGotchi.Domain;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Interfaces
{
    public interface IPetsStatusesDbContext
    {
        DbSet<PetStatus> PetsStatuses { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
