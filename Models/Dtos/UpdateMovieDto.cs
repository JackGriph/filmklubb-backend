using System.ComponentModel.DataAnnotations;

namespace filmklubb_backend.Models.Dtos;

public class UpdateMovieDto : IValidatableObject
{
    [Required(ErrorMessage = "Titel är obligatorisk.")]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    public MediaType Type { get; set; }

    public bool Watched { get; set; }

    [Range(1, 5, ErrorMessage = "Betyg måste vara mellan 1 och 5.")]
    public int? Rating { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Rating.HasValue && !Watched)
        {
            yield return new ValidationResult(
                "Betyg kan bara sättas på något som är markerat som sett.",
                [nameof(Rating), nameof(Watched)]);
        }
    }
}