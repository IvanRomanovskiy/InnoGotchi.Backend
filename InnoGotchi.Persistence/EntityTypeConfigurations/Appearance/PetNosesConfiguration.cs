using InnoGotchi.Domain.Appearance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Persistence.EntityTypeConfigurations.Appearance
{
    public class PetNosesConfiguration : IEntityTypeConfiguration<Noses>
    {
        public void Configure(EntityTypeBuilder<Noses> builder)
        {
            builder.HasKey(key => key.Path);
            builder.HasData(new Eyes { Path = @"/Components/Images/Noses/nose1.svg" });
            builder.HasData(new Eyes { Path = @"/Components/Images/Noses/nose2.svg" });
            builder.HasData(new Eyes { Path = @"/Components/Images/Noses/nose3.svg" });
            builder.HasData(new Eyes { Path = @"/Components/Images/Noses/nose4.svg" });
            builder.HasData(new Eyes { Path = @"/Components/Images/Noses/nose5.svg" });
            builder.HasData(new Eyes { Path = @"/Components/Images/Noses/nose6.svg" });
        }
    }
}
