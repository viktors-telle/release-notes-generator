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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().GetTypeInfo().Assembly);
        }
    }
}
