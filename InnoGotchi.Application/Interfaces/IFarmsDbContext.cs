using InnoGotchi.Domain;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Interfaces
{
    public interface IFarmsDbContext
    {
        DbSet<Farm> Farms { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
