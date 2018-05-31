﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ReleaseNotesGenerator.Dal;
using ReleaseNotesGenerator.Enums;
using System;

namespace ReleaseNotesGenerator.Migrations
{
    [DbContext(typeof(ReleaseNotesContext))]
    [Migration("20180301213557_ChangeFromAndToDatesToNullable")]
    partial class ChangeFromAndToDatesToNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReleaseNotesGenerator.Features.Projects.Project", b =>
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

            modelBuilder.Entity("ReleaseNotesGenerator.Features.ProjectTrackingTools.ProjectTrackingTool", b =>
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

            modelBuilder.Entity("ReleaseNotesGenerator.Features.ReleaseNotes.ReleaseNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("From")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .IsRequired();

                    b.Property<int>("RepositoryId");

                    b.Property<string>("RepositoryPath")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("Until")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RepositoryId");

                    b.ToTable("ReleaseNotes","rng");
                });

            modelBuilder.Entity("ReleaseNotesGenerator.Features.SourceCodeRepositories.Repository", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("Owner")
                        .HasMaxLength(128);

                    b.Property<int>("ProjectId");

                    b.Property<int?>("ProjectTrackingToolId");

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

            modelBuilder.Entity("ReleaseNotesGenerator.Features.ReleaseNotes.ReleaseNote", b =>
                {
                    b.HasOne("ReleaseNotesGenerator.Features.SourceCodeRepositories.Repository", "Repository")
                        .WithMany("ReleaseNotes")
                        .HasForeignKey("RepositoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReleaseNotesGenerator.Features.SourceCodeRepositories.Repository", b =>
                {
                    b.HasOne("ReleaseNotesGenerator.Features.Projects.Project", "Project")
                        .WithMany("Repositories")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReleaseNotesGenerator.Features.ProjectTrackingTools.ProjectTrackingTool", "ProjectTrackingTool")
                        .WithMany("Repositories")
                        .HasForeignKey("ProjectTrackingToolId");
                });
#pragma warning restore 612, 618
        }
    }
}
