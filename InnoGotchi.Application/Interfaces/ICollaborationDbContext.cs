using InnoGotchi.Domain;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Interfaces
{
    public interface ICollaborationDbContext
    {
        DbSet<Collaboration> Collaborations { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
