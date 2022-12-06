using InnoGotchi.Domain.Appearance;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Interfaces.Appearance
{
    public interface INosesDbContext
    {
        public DbSet<Noses> Noses { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
