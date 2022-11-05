using InnoGotchi.Application.Interfaces.Appearance;
using InnoGotchi.Domain;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Interfaces
{
    public interface IPetAppearanceDbContext : IBodiesDbContext, IEyesDbContext, IMouthsDbContext, INosesDbContext
    {
        DbSet<PetAppearance> PetAppearances { get; set; }
        new Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
