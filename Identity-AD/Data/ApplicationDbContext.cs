using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
 using CustomIdentityServer4.Controllers;

namespace IdentityServer4Admin.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserToken> Tokens { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}


// add-migration -context ApplicationDbContext InitialDbMigration -o Data/Migrations/ApplicationDb
//PM > update-database -context ApplicationDbContext 
// add-migration -context ConfigurationDbContext InitialDbMigration -o Data/Migrations/ConfigurationDb
//PM > update-database -context ConfigurationDbContext
// add-migration -context PersistedGrantDbContext InitialDbMigration -o Data/Migrations/PersistedGrantDb
//update-database -context PersistedGrantDbContext
