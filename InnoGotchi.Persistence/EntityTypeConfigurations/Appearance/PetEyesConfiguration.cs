using InnoGotchi.Domain.Appearance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Persistence.EntityTypeConfigurations.Appearance
{
    public class PetEyesConfiguration : IEntityTypeConfiguration<Eyes>
    {
        public void Configure(EntityTypeBuilder<Eyes> builder)
        {
            builder.HasKey(key => key.Path);
            builder.HasData(new Eyes { Path = @"/Components/Images/Eyes/eye1.svg" });
            builder.HasData(new Eyes { Path = @"/Components/Images/Eyes/eye2.svg" });
            builder.HasData(new Eyes { Path = @"/Components/Images/Eyes/eye3.svg" });
            builder.HasData(new Eyes { Path = @"/Components/Images/Eyes/eye4.svg" });
        }
    }
}
