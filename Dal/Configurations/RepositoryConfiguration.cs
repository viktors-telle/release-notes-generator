using ReleaseNotesGenerator.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ReleaseNotesGenerator.Dal.Configurations
{
    public class RepositoryConfiguration : EntityMappingConfiguration<Repository>
    {
        public override void Map(EntityTypeBuilder<Repository> b)
        {
            b.ToTable("Repositories", "rng")
              .HasKey(p => p.Id);

            b.Property(p => p.Name).HasMaxLength(256).IsRequired();            
            b.Property(p => p.AccessToken).HasMaxLength(512).IsRequired();
            b.Property(p => p.Url).HasMaxLength(4096).IsRequired();

            b.HasOne(r => r.Project).WithMany(p => p.Repositories);
            b.HasOne(r => r.ProjectTrackingTool).WithMany(p => p.Repositories);
            b.HasOne(r => r.RepositoryType).WithMany(p => p.Repositories);
        }
    }
}
