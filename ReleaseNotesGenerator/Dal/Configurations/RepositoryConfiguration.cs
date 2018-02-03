using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReleaseNotesGenerator.Features.SourceCodeRepositories;

namespace ReleaseNotesGenerator.Dal.Configurations
{
    public class RepositoryConfiguration : EntityMappingConfiguration<Repository>
    {
        public override void Map(EntityTypeBuilder<Repository> b)
        {
            b.ToTable("Repositories", "rng")
              .HasKey(p => p.Id);

            b.Property(p => p.Name).HasMaxLength(256).IsRequired();
            b.Property(p => p.Url).HasMaxLength(2048).IsRequired();
            b.Property(p => p.Owner).HasMaxLength(128);

            b.HasIndex(p => new { p.Name, p.Url }).IsUnique();
            b.HasOne(r => r.Project).WithMany(p => p.Repositories);
            b.HasOne(r => r.ProjectTrackingTool).WithMany(p => p.Repositories);
        }
    }
}
