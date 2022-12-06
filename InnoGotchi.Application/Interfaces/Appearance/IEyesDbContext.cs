using InnoGotchi.Domain.Appearance;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Interfaces.Appearance
{
    public interface IEyesDbContext
    {
        public DbSet<Eyes> Eyes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
