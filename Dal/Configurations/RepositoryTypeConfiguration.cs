using ReleaseNotesGenerator.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ReleaseNotesGenerator.Dal.Configurations
{
    public class RepositoryTypeConfiguration : EntityMappingConfiguration<RepositoryType>
    {
        public override void Map(EntityTypeBuilder<RepositoryType> b)
        {
            b.ToTable("RepositoryTypes", "rng")
              .HasKey(p => p.Id);

            b.Property(p => p.Name).HasMaxLength(256).IsRequired();            
        }
    }
}
