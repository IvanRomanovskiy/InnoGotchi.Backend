using InnoGotchi.Domain.Appearance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Persistence.EntityTypeConfigurations.Appearance
{
    public class PetMouthsConfiguration : IEntityTypeConfiguration<Mouths>
    {
        public void Configure(EntityTypeBuilder<Mouths> builder)
        {
            builder.HasKey(key => key.Path);
            builder.HasData(new Eyes { Path = @"/Components/Images/Mouths/mouth1.svg" });
            builder.HasData(new Eyes { Path = @"/Components/Images/Mouths/mouth2.svg" });
            builder.HasData(new Eyes { Path = @"/Components/Images/Mouths/mouth3.svg" });
            builder.HasData(new Eyes { Path = @"/Components/Images/Mouths/mouth4.svg" });
            builder.HasData(new Eyes { Path = @"/Components/Images/Mouths/mouth5.svg" });
        }
    }
}
