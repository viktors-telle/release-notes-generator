using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReleaseNotes.Generator.Domain;

namespace ReleaseNotes.Generator.Dal.Configurations
{
    public class BranchConfiguration : EntityMappingConfiguration<Branch>
    {
        public override void Map(EntityTypeBuilder<Branch> b)
        {
            b.ToTable("Branches", "rng").HasKey(p => p.Id);

            b.Property(p => p.Name).HasMaxLength(256).IsRequired();
            b.Property(p => p.LastCommitId).HasMaxLength(512);
            b.Property(p => p.LastCommitDateTime).HasColumnType("datetime2");           
             
            b.HasIndex(p => new { p.Name, p.RepositoryId}).IsUnique();
            b.HasOne(branch => branch.Repository).WithMany(r => r.Branches);
        }
    }
}
