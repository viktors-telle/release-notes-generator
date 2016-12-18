using ReleaseNotesGenerator.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ReleaseNotesGenerator.Dal.Configurations
{
    public class ProjectConfiguration : EntityMappingConfiguration<Project>
    {
        public override void Map(EntityTypeBuilder<Project> b)
        {
            b.ToTable("Projects", "rng")
              .HasKey(p => p.Id);

            b.Property(p => p.Name).HasMaxLength(256).IsRequired();
            b.Property(p => p.ApiKey).HasMaxLength(128).IsRequired();
            b.Property(p => p.IsDeactivated).IsRequired();
        }
    }
}
