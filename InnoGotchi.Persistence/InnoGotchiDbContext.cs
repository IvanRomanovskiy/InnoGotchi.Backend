using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;
using InnoGotchi.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Persistence
{
    public class InnoGotchiDbContext : DbContext, IUsersDbContext
    {
        public DbSet<User> Users { get; set; }

        public InnoGotchiDbContext(DbContextOptions<InnoGotchiDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(builder);
        }

    }
}
