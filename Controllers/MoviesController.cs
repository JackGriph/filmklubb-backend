using filmklubb_backend.Data;
using filmklubb_backend.Models;
using filmklubb_backend.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace filmklubb_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly FilmklubbContext _context;

    public MoviesController(FilmklubbContext context)
    {
        _context = context;
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
}

