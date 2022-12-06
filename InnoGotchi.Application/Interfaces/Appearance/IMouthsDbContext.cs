using InnoGotchi.Domain.Appearance;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Interfaces.Appearance
{
    public interface IMouthsDbContext
    {
        public DbSet<Mouths> Mouths { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
