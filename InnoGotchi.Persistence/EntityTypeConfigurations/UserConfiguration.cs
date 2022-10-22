using Microsoft.EntityFrameworkCore;
using InnoGotchi.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Persistence.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(user => user.Id).IsUnique();
            builder.Property(user => user.FirstName).HasMaxLength(30);
            builder.Property(user => user.LastName).HasMaxLength(30);
        }
    }
}
