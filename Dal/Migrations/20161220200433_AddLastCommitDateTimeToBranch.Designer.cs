using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ReleaseNotesGenerator.Dal;
using ReleaesNotesGenerator.Common.Enums;

namespace ReleaseNotesGenerator.Dal.Migrations
{
    [DbContext(typeof(ReleaseNotesContext))]
    [Migration("20161220200433_AddLastCommitDateTimeToBranch")]
    partial class AddLastCommitDateTimeToBranch
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReleaseNotesGenerator.Domain.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastCommitDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastCommitId")
                        .HasMaxLength(512);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("RepositoryId");

                    b.HasKey("Id");

                    b.HasIndex("RepositoryId");

                    b.ToTable("Branches","rng");
                });

            modelBuilder.Entity("ReleaseNotesGenerator.Domain.Project", b =>
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

                    b.ToTable("Projects","rng");
                });

            modelBuilder.Entity("ReleaseNotesGenerator.Domain.ProjectTrackingTool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(4096);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("ProjectTrackingTools","rng");
                });

            modelBuilder.Entity("ReleaseNotesGenerator.Domain.Repository", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("ProjectId");

                    b.Property<int>("ProjectTrackingToolId");

                    b.Property<int>("RepositoryTypeId");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(4096);

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectTrackingToolId");

                    b.HasIndex("RepositoryTypeId");

                    b.ToTable("Repositories","rng");
                });

            modelBuilder.Entity("ReleaseNotesGenerator.Domain.RepositoryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("RepositoryTypes","rng");
                });

            modelBuilder.Entity("ReleaseNotesGenerator.Domain.Branch", b =>
                {
                    b.HasOne("ReleaseNotesGenerator.Domain.Repository", "Repository")
                        .WithMany("Branches")
                        .HasForeignKey("RepositoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReleaseNotesGenerator.Domain.Repository", b =>
                {
                    b.HasOne("ReleaseNotesGenerator.Domain.Project", "Project")
                        .WithMany("Repositories")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReleaseNotesGenerator.Domain.ProjectTrackingTool", "ProjectTrackingTool")
                        .WithMany("Repositories")
                        .HasForeignKey("ProjectTrackingToolId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReleaseNotesGenerator.Domain.RepositoryType", "RepositoryType")
                        .WithMany("Repositories")
                        .HasForeignKey("RepositoryTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
