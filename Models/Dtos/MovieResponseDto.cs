namespace filmklubb_backend.Models.Dtos;

public class MovieResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public MediaType Type { get; set; }
    public bool Watched { get; set; }
    public int? Rating { get; set; }
    public string? Notes { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? WatchedAt { get; set; }

    public static MovieResponseDto FromMovie(Movie movie) => new()
    {
        Id = movie.Id,
        Title = movie.Title,
        Type = movie.Type,
        Watched = movie.Watched,
        Rating = movie.Rating,
        Notes = movie.Notes,
        ImageUrl = movie.ImageUrl,
        CreatedAt = movie.CreatedAt,
        WatchedAt = movie.WatchedAt
    };
}