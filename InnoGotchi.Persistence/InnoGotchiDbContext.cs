using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;
using InnoGotchi.Domain.Appearance;
using InnoGotchi.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Persistence
{
    public class InnoGotchiDbContext : DbContext, 
        IUsersDbContext,IPetsDbContext,IPetsStatusesDbContext,IFarmsDbContext, IPetAppearanceDbContext, ICollaborationDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<PetStatus> PetsStatuses { get; set; }

        public DbSet<Farm> Farms { get; set; }

        public DbSet<Bodies> Bodies { get; set; }
        public DbSet<Eyes> Eyes { get; set; }
        public DbSet<Mouths> Mouths { get; set; }
        public DbSet<Noses> Noses { get; set; }
        public DbSet<PetAppearance> PetAppearances { get; set; }
        public DbSet<Collaboration> Collaborations { get; set; }

        public InnoGotchiDbContext(DbContextOptions<InnoGotchiDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new FarmConfiguration());
            builder.ApplyConfiguration(new CollaborationConfiguration());
            builder.ApplyConfiguration(new PetConfiguration());
            builder.ApplyConfiguration(new PetStatusConfiguration());
            base.OnModelCreating(builder);
        }

    }
}
