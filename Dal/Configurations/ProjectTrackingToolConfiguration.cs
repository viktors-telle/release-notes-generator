using ReleaseNotesGenerator.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ReleaseNotesGenerator.Dal.Configurations
{
    public class ProjectTrackingToolConfiguration : EntityMappingConfiguration<ProjectTrackingTool>
    {
        public override void Map(EntityTypeBuilder<ProjectTrackingTool> b)
        {
            b.ToTable("ProjectTrackingTools", "rng")
              .HasKey(p => p.Id);

            b.Property(p => p.Name).HasMaxLength(256).IsRequired();
            b.Property(p => p.UserName).HasMaxLength(128).IsRequired();
            b.Property(p => p.Password).HasMaxLength(512).IsRequired();
            b.Property(p => p.Url).HasMaxLength(4096).IsRequired();
        }
    }
}
