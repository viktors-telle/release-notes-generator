using Microsoft.EntityFrameworkCore;
using ReleaseNotesGenerator.Dal.Extensions;
using ReleaseNotesGenerator.Domain;
using System.Reflection;

namespace ReleaseNotesGenerator.Dal
{
    public class ReleaseNotesContext : DbContext
    {
        public ReleaseNotesContext(DbContextOptions<ReleaseNotesContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Repository> Repositories { get; set; }

        public DbSet<ProjectTrackingTool> ProjectTrackingTools { get; set; }

        public DbSet<RepositoryType> RepositoryTypes { get; set; }

        public DbSet<Branch> Branches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().GetTypeInfo().Assembly);
        }
    }
}
