using InnoGotchi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Persistence.EntityTypeConfigurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(pet => pet.Id);
            builder.HasIndex(pet => pet.Id).IsUnique();
            builder.HasIndex(pet => pet.Name).IsUnique();
            builder.Property(pet => pet.Name).HasMaxLength(30);
            builder.HasOne(pet => pet.Status).WithOne().HasPrincipalKey<Pet>(key => key.Id);
            builder.HasOne(pet => pet.Appearance).WithOne().HasPrincipalKey<PetAppearance>(key => key.Id);
        }
    }
}
