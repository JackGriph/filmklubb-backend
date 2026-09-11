using filmklubb_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace filmklubb_backend.Data;

public class FilmklubbContext : DbContext
{
    public FilmklubbContext(DbContextOptions<FilmklubbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies => Set<Movie>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>()
            .Property(m => m.Type)
            .HasConversion<string>()
            .HasMaxLength(20);

        modelBuilder.Entity<Movie>().HasData(
            new Movie
            {
                Id = 1,
                Title = "Dune: Part Two",
                Type = MediaType.Film,
                Watched = true,
                Rating = 5,
                Notes = "Bäst på stor duk. Ljudmixen bär hela filmen.",
                CreatedAt = new DateTime(2026, 1, 10, 18, 0, 0, DateTimeKind.Utc),
                WatchedAt = new DateTime(2026, 1, 18, 20, 30, 0, DateTimeKind.Utc)
            },
            new Movie
            {
                Id = 2,
                Title = "Severance",
                Type = MediaType.Serie,
                Watched = true,
                Rating = 4,
                Notes = "Säsong 1 håller hela vägen. Långsam start.",
                CreatedAt = new DateTime(2026, 1, 12, 9, 15, 0, DateTimeKind.Utc),
                WatchedAt = new DateTime(2026, 2, 2, 21, 0, 0, DateTimeKind.Utc)
            },
            new Movie
            {
                Id = 3,
                Title = "The Brutalist",
                Type = MediaType.Film,
                Watched = false,
                Notes = "Tre och en halv timme - boka in en hel kväll.",
                CreatedAt = new DateTime(2026, 2, 5, 12, 0, 0, DateTimeKind.Utc)
            },
            new Movie
            {
                Id = 4,
                Title = "Shogun",
                Type = MediaType.Serie,
                Watched = false,
                CreatedAt = new DateTime(2026, 2, 20, 8, 45, 0, DateTimeKind.Utc)
            }
        );
    }
}
