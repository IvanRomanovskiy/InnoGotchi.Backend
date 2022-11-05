using InnoGotchi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Persistence.EntityTypeConfigurations
{
    public class FarmConfiguration : IEntityTypeConfiguration<Farm>
    {
        public void Configure(EntityTypeBuilder<Farm> builder)
        {
            builder.HasKey(farm => farm.Id);
            builder.HasIndex(farm => farm.Id).IsUnique();
            builder.HasIndex(farm => farm.Name).IsUnique();
            builder.Property(farm => farm.Name).HasMaxLength(30);
            builder.HasOne(farm => farm.Owner).WithOne().HasPrincipalKey<User>(key => key.Id);
        }
    }
}
