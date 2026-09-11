using filmklubb_backend.Data;
using filmklubb_backend.Models;
using filmklubb_backend.Models.Dtos;
using filmklubb_backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace filmklubb_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly FilmklubbContext _context;
    private readonly FileStorageService _fileStorage;

    public MoviesController(FilmklubbContext context, FileStorageService fileStorage)
    {
        _context = context;
        _fileStorage = fileStorage;
    }

    //GET api/movies
    //GET api/movies?watched=true

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieResponseDto>>> GetMovies([FromQuery] bool? watched)
    {
        var query = _context.Movies.AsNoTracking();

        if (watched.HasValue)
        {
            query = query.Where(m => m.Watched == watched.Value);
        }

        var movies = await query
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();

        return Ok(movies.Select(MovieResponseDto.FromMovie));
    }

    // GET /api/movies/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovieResponseDto>> GetMovie(int id)
    {
        var movie = await _context.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie is null)
        {
            return NotFound();
        }

        return Ok(MovieResponseDto.FromMovie(movie));
    }

    // POST /api/movies
    [HttpPost]
    public async Task<ActionResult<MovieResponseDto>> CreateMovie(CreateMovieDto dto)
    {
        var movie = new Movie
        {
            Title = dto.Title.Trim(),
            Type = dto.Type,
            Notes = string.IsNullOrWhiteSpace(dto.Notes) ? null : dto.Notes.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetMovie),
            new { id = movie.Id },
            MovieResponseDto.FromMovie(movie));
    }

    // PUT /api/movies/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<MovieResponseDto>> UpdateMovie(int id, UpdateMovieDto dto)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

        if (movie is null)
        {
            return NotFound();
        }

        // Måste beräknas innan movie.Watched skrivs över.
        var isNewlyWatched = dto.Watched && !movie.Watched;

        movie.Title = dto.Title.Trim();
        movie.Type = dto.Type;
        movie.Notes = string.IsNullOrWhiteSpace(dto.Notes) ? null : dto.Notes.Trim();
        movie.Watched = dto.Watched;

        if (dto.Watched)
        {
            movie.Rating = dto.Rating;

            if (isNewlyWatched)
            {
                movie.WatchedAt = DateTime.UtcNow;
            }
        }
        else
        {
            movie.Rating = null;
            movie.WatchedAt = null;
        }

        await _context.SaveChangesAsync();

        return Ok(MovieResponseDto.FromMovie(movie));
    }

        // POST /api/movies/5/image   (multipart/form-data)
    [HttpPost("{id:int}/image")]
    public async Task<ActionResult<MovieResponseDto>> UploadImage(int id, IFormFile file)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

        if (movie is null)
        {
            return NotFound();
        }

        if (file is null)
        {
            ModelState.AddModelError(nameof(file), "Ingen fil bifogad.");
            return ValidationProblem(ModelState);
        }

        var validationError = _fileStorage.Validate(file);

        if (validationError is not null)
        {
            ModelState.AddModelError(nameof(file), validationError);
            return ValidationProblem(ModelState);
        }

        var previousImageUrl = movie.ImageUrl;

        movie.ImageUrl = await _fileStorage.SaveAsync(file);
        await _context.SaveChangesAsync();

        // Gamla filen tas bort först när databasen pekar på den nya.
        _fileStorage.Delete(previousImageUrl);

        return Ok(MovieResponseDto.FromMovie(movie));
    }
}

