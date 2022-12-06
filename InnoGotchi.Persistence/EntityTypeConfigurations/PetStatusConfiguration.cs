using InnoGotchi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Persistence.EntityTypeConfigurations
{
    public class PetStatusConfiguration : IEntityTypeConfiguration<PetStatus>
    {
        public void Configure(EntityTypeBuilder<PetStatus> builder)
        {
            builder.HasKey(status => status.Id);
            builder.HasIndex(status => status.Id).IsUnique();
        }
    }
}
