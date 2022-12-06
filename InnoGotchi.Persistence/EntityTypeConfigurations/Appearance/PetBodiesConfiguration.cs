using InnoGotchi.Domain.Appearance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Persistence.EntityTypeConfigurations.Appearance
{
    public class PetBodiesConfiguration : IEntityTypeConfiguration<Bodies>
    {
        public void Configure(EntityTypeBuilder<Bodies> builder)
        {
            builder.HasKey(key => key.Path);
            builder.HasData(new Bodies { Path = @"/Components/Images/Bodies/body1.svg" });
            builder.HasData(new Bodies { Path = @"/Components/Images/Bodies/body2.svg" });
        }
    }
}
