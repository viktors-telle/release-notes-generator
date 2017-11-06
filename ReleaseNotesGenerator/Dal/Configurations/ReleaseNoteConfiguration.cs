using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Dal.Configurations
{
    public class ReleaseNoteConfiguration : EntityMappingConfiguration<ReleaseNote>
    {
        public override void Map(EntityTypeBuilder<ReleaseNote> b)
        {
            b.ToTable("ReleaseNotes", "rng").HasKey(p => p.Id);

            b.Property(p => p.Notes).IsRequired();
            b.Property(p => p.Created).IsRequired().HasColumnType("datetime2");           
             
            b.HasOne(p => p.Repository).WithMany(r => r.ReleaseNotes);
        }
    }
}
