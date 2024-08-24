using Microsoft.EntityFrameworkCore;
using Calendar.Models.GoogleCalendar;

namespace Calendar.DataAccess.Imp
{
    public class GoogleTokenContext : DbContext
    {
        public GoogleTokenContext(DbContextOptions<GoogleTokenContext> options)
            : base(options)
        {
        }

        public DbSet<GoogleTokenResponse> GoogleTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the GoogleTokenResponse entity
            modelBuilder.Entity<GoogleTokenResponse>(entity =>
            {
                entity.HasKey(e => e.Id); // Set the primary key as GUID
                entity.Property(e => e.AccessToken).IsRequired(); // Ensure AccessToken is required
                entity.Property(e => e.RefreshToken).IsRequired(); // Ensure RefreshToken is required
                entity.Property(e => e.TokenIssued).IsRequired(); // Ensure TokenIssued is required
                entity.Property(e => e.Id).HasDefaultValueSql("NEWID()"); // Set default GUID generation
            });
        }
    }
}
