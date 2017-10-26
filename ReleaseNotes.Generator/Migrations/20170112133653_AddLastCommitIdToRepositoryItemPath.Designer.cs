using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ReleaseNotes.Generator.Dal;
using ReleaseNotes.Generator.Enums;

namespace ReleaseNotes.Generator.Migrations
{
    [DbContext(typeof(ReleaseNotesContext))]
    [Migration("20170112133653_AddLastCommitIdToRepositoryItemPath")]
    partial class AddLastCommitIdToRepositoryItemPath
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReleaseNotes.Generator.Domain.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("LastCommitDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastCommitId")
                        .HasMaxLength(512);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("RepositoryId");

                    b.HasKey("Id");

                    b.HasIndex("RepositoryId");

                    b.HasIndex("Name", "RepositoryId")
                        .IsUnique();

                    b.ToTable("Branches","rng");
                });

            modelBuilder.Entity("ReleaseNotes.Generator.Domain.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<bool>("IsDeactivated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("ApiKey")
                        .IsUnique();

                    b.HasIndex("IsDeactivated");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Projects","rng");
                });

            modelBuilder.Entity("ReleaseNotes.Generator.Domain.ProjectTrackingTool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("Type");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(2048);

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("ProjectTrackingTools","rng");
                });

            modelBuilder.Entity("ReleaseNotes.Generator.Domain.Repository", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("ProjectId");

                    b.Property<int>("ProjectTrackingToolId");

                    b.Property<int>("RepositoryType");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(2048);

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectTrackingToolId");

                    b.HasIndex("Name", "Url")
                        .IsUnique();

                    b.ToTable("Repositories","rng");
                });

            modelBuilder.Entity("ReleaseNotes.Generator.Domain.RepositoryItemPath", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LastCommitId")
                        .HasMaxLength(512);

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("RepositoryId");

                    b.HasKey("Id");

                    b.HasIndex("RepositoryId");

                    b.HasIndex("Path", "RepositoryId")
                        .IsUnique();

                    b.ToTable("RepositoryItemPaths","rng");
                });

            modelBuilder.Entity("ReleaseNotes.Generator.Domain.Branch", b =>
                {
                    b.HasOne("ReleaseNotes.Generator.Domain.Repository", "Repository")
                        .WithMany("Branches")
                        .HasForeignKey("RepositoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReleaseNotes.Generator.Domain.Repository", b =>
                {
                    b.HasOne("ReleaseNotes.Generator.Domain.Project", "Project")
                        .WithMany("Repositories")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReleaseNotes.Generator.Domain.ProjectTrackingTool", "ProjectTrackingTool")
                        .WithMany("Repositories")
                        .HasForeignKey("ProjectTrackingToolId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReleaseNotes.Generator.Domain.RepositoryItemPath", b =>
                {
                    b.HasOne("ReleaseNotes.Generator.Domain.Repository", "Repository")
                        .WithMany("RepositoryItemPaths")
                        .HasForeignKey("RepositoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
