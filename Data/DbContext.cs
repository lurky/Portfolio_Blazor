using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Portfolio_Blazor
{

    public class DataContext : DbContext
    {
        public static readonly string PortfolioDb = nameof(PortfolioDb).ToLower();

        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString($"Datas Source={PortfolioDb}"));
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Technology> Technologies { get; set; }

        // Define the model.
        // modelBuilder: The ModelBuilder.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .Property(p => p.Title)
                .IsRequired();
            modelBuilder.Entity<Project>()
                .Property(p => p.Description)
                .IsRequired();
            modelBuilder.Entity<Project>()
                .Property(p => p.StartDate)
                .IsRequired();
            modelBuilder.Entity<Project>()
                .Property(p => p.EndDate)
                .IsRequired();
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Technologies)
                .WithMany(t => t.Projects);

            modelBuilder.Entity<Technology>()
                .Property(t => t.Name)
                .IsRequired();
            modelBuilder.Entity<Technology>()
                .Property(t => t.Description)
                .IsRequired();
            modelBuilder.Entity<Technology>()
                .Property(t => t.Icon)
                .IsRequired();
            modelBuilder.Entity<Technology>()
                .Property(t => t.Url)
                .IsRequired();
            modelBuilder.Entity<Technology>()
                .HasMany(t => t.Projects)
                .WithMany(p => p.Technologies);

            base.OnModelCreating(modelBuilder);
        }

        // Dispose pattern.
        public override void Dispose()
        {
            Debug.WriteLine($"{ContextId} context disposed.");
            base.Dispose();
        }

        // Dispose pattern.
        public override ValueTask DisposeAsync()
        {
            Debug.WriteLine($"{ContextId} context disposed async.");
            return base.DisposeAsync();
        }
    }
}
