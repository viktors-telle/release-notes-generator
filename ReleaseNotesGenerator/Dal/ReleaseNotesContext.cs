using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReleaseNotesGenerator.Dal.Extensions;
using ReleaseNotesGenerator.Features.Projects;
using ReleaseNotesGenerator.Features.ProjectTrackingTools;
using ReleaseNotesGenerator.Features.ReleaseNotes;
using ReleaseNotesGenerator.Features.SourceCodeRepositories;

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

        public DbSet<ReleaseNote> ReleaseNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
        }

        public static void Migrate(IApplicationBuilder app)
        {         
            using (var context = app.ApplicationServices.GetService<ReleaseNotesContext>())
            {                
                context.Database.Migrate();              
            }
        }
    }
}
