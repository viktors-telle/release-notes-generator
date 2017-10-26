using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReleaseNotes.Generator.Dal.Extensions;
using ReleaseNotes.Generator.Domain;

namespace ReleaseNotes.Generator.Dal
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

        public DbSet<Branch> Branches { get; set; }

        public DbSet<RepositoryItemPath> RepositoryItemPaths { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
        }

        public static void Migrate(IApplicationBuilder app)
        {         
            using (var context = app.ApplicationServices.GetRequiredService<ReleaseNotesContext>())
            {                
                context.Database.Migrate();              
            }
        }
    }
}
