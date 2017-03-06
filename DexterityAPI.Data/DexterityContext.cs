using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DexterityAPI.Data
{
    using Domain;

    public class DexterityContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DexterityContext(DbContextOptions options) : base(options) { }
        public DexterityContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DexterityDB"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetDatabaseSpecificsForUserEntity(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SetDatabaseSpecificsForUserEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Forename).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Surname).HasMaxLength(100).IsRequired();

            // Defining 320 characters as a sensible maximum size for an email address based on 64 characters for local part (username),
            // 1 character for @ symbol and 255 characters for domain name.
            modelBuilder.Entity<User>().Property(u => u.EmailAddress).HasColumnType("VARCHAR(320)").IsRequired();

            // Each user must have a unique email address.
            modelBuilder.Entity<User>().HasIndex(i => i.EmailAddress).IsUnique();
        }
    }
}
