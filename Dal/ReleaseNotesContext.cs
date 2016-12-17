using Microsoft.EntityFrameworkCore;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Dal
{
    public class ReleaseNotesContext : DbContext
    {
        public ReleaseNotesContext(DbContextOptions<ReleaseNotesContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
    }
}
