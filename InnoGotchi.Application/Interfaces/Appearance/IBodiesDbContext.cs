using InnoGotchi.Domain.Appearance;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Interfaces.Appearance
{
    public interface IBodiesDbContext
    {
        public DbSet<Bodies> Bodies { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
