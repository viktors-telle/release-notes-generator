﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReleaseNotesGenerator.Features.ProjectTrackingTools;

namespace ReleaseNotesGenerator.Dal.Configurations
{
    public class ProjectTrackingToolConfiguration : EntityMappingConfiguration<ProjectTrackingTool>
    {
        public override void Map(EntityTypeBuilder<ProjectTrackingTool> b)
        {
            b.ToTable("ProjectTrackingTools", "rng")
              .HasKey(p => p.Id);

            b.Property(p => p.Name).HasMaxLength(256).IsRequired();            
            b.Property(p => p.Url).HasMaxLength(2048).IsRequired();
            b.Property(p => p.ProjectName).HasMaxLength(256).IsRequired();

            b.HasAlternateKey(p => p.Name);
        }
    }
}
