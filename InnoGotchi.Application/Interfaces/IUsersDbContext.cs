using InnoGotchi.Domain;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Interfaces
{
    public interface IUsersDbContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
