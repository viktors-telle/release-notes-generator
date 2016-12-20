using ReleaseNotesGenerator.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ReleaseNotesGenerator.Dal.Configurations
{
    public class BranchConfiguration : EntityMappingConfiguration<Branch>
    {
        public override void Map(EntityTypeBuilder<Branch> b)
        {
            b.ToTable("Branches", "rng")
              .HasKey(p => p.Id);

            b.Property(p => p.Name).HasMaxLength(256).IsRequired();
            b.Property(p => p.LastCommitId).HasMaxLength(512);
            b.Property(p => p.LastCommitDateTime).HasColumnType("datetime2");

            b.HasOne(branch => branch.Repository).WithMany(r => r.Branches);
        }
    }
}
