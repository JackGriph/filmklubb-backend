using System.ComponentModel.DataAnnotations;

namespace filmklubb_backend.Models.Dtos;

public class CreateMovieDto
{
    [Required(ErrorMessage = "Titel är obligatorisk.")]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    public MediaType Type { get; set; } = MediaType.Film;

    [StringLength(1000)]
    public string? Notes { get; set; }

}