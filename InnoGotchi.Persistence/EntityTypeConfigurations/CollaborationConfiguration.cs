using InnoGotchi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace InnoGotchi.Persistence.EntityTypeConfigurations
{
    public class CollaborationConfiguration : IEntityTypeConfiguration<Collaboration>
    {
        public void Configure(EntityTypeBuilder<Collaboration> builder)
        {
            builder.HasKey(key => new { key.IdCollaborator, key.IdOwner });
        }
    }
}
