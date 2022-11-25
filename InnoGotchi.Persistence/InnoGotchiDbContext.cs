using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;
using InnoGotchi.Domain.Appearance;
using InnoGotchi.Persistence.EntityTypeConfigurations;
using InnoGotchi.Persistence.EntityTypeConfigurations.Appearance;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Persistence
{
    public class InnoGotchiDbContext : DbContext, 
        IUsersDbContext,IPetsDbContext,IPetsStatusesDbContext,IFarmsDbContext, IPetAppearanceDbContext, ICollaborationDbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Pet> Pets { get; set; }

        public virtual DbSet<PetStatus> PetsStatuses { get; set; }

        public virtual DbSet<Farm> Farms { get; set; }

        public virtual DbSet<Bodies> Bodies { get; set; }
        public virtual DbSet<Eyes> Eyes { get; set; }
        public virtual DbSet<Mouths> Mouths { get; set; }
        public virtual DbSet<Noses> Noses { get; set; }
        public virtual DbSet<PetAppearance> PetAppearances { get; set; }
        public virtual DbSet<Collaboration> Collaborations { get; set; }

        public InnoGotchiDbContext(DbContextOptions<InnoGotchiDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new FarmConfiguration());
            builder.ApplyConfiguration(new CollaborationConfiguration());
            builder.ApplyConfiguration(new PetBodiesConfiguration());
            builder.ApplyConfiguration(new PetEyesConfiguration());
            builder.ApplyConfiguration(new PetNosesConfiguration());
            builder.ApplyConfiguration(new PetMouthsConfiguration());
            builder.ApplyConfiguration(new PetConfiguration());
            builder.ApplyConfiguration(new PetStatusConfiguration());
            base.OnModelCreating(builder);


        }

    }
}
