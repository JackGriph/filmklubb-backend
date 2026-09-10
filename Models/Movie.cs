using System.ComponentModel.DataAnnotations;

namespace filmklubb_backend.Models;

public enum MediaType
{
    Film,
    Serie
}

public class Movie
{
    public int Id { get; set; }

    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public MediaType Type { get; set; } = MediaType.Film;

    public bool Watched { get; set; }

    public int? Rating { get; set; }

    [MaxLength(1000)]
    public string? Notes { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? WatchedAt { get; set; }
}