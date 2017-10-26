using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReleaseNotes.Generator.Domain;

namespace ReleaseNotes.Generator.Dal.Configurations
{
    public class RepositoryItemPathConfiguration : EntityMappingConfiguration<RepositoryItemPath>
    {
        public override void Map(EntityTypeBuilder<RepositoryItemPath> b)
        {
            b.ToTable("RepositoryItemPaths", "rng").HasKey(p => p.Id);

            b.Property(p => p.Path).HasMaxLength(256).IsRequired();
            b.Property(p => p.LastCommitId).HasMaxLength(512);

            b.HasIndex(p => new { p.Path, p.BranchId}).IsUnique();
            b.HasOne(repositoryItemPath => repositoryItemPath.Branch).WithMany(r => r.RepositoryItemPaths);
        }
    }
}
