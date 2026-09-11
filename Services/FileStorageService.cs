namespace filmklubb_backend.Services;

public class FileStorageService
{
    private static readonly string[] AllowedExtensions =
        [".jpg", ".jpeg", ".png", ".webp", ".gif"];

    private const long MaxBytes = 5 * 1024 * 1024;

    private readonly string _uploadPath;

    public FileStorageService(IWebHostEnvironment env)
    {
        var webRoot = env.WebRootPath ?? Path.Combine(env.ContentRootPath, "wwwroot");
        _uploadPath = Path.Combine(webRoot, "uploads");
        Directory.CreateDirectory(_uploadPath);
    }

    /// <summary>Returnerar null om filen är godkänd, annars ett felmeddelande.</summary>
    public string? Validate(IFormFile file)
    {
        if (file.Length == 0)
        {
            return "Filen är tom.";
        }

        if (file.Length > MaxBytes)
        {
            return $"Filen är för stor. Max {MaxBytes / 1024 / 1024} MB.";
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!AllowedExtensions.Contains(extension))
        {
            return $"Filtypen stöds inte. Tillåtna format: {string.Join(", ", AllowedExtensions)}.";
        }

        return null;
    }

    public async Task<string> SaveAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fileName = $"{Guid.NewGuid():N}{extension}";
        var fullPath = Path.Combine(_uploadPath, fileName);

        await using var stream = File.Create(fullPath);
        await file.CopyToAsync(stream, cancellationToken);

        return $"/uploads/{fileName}";
    }

    public void Delete(string? imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
        {
            return;
        }

        // GetFileName kapar bort eventuella ../ - filnamnet får aldrig
        // peka utanför uploads-mappen.
        var fileName = Path.GetFileName(imageUrl);
        var fullPath = Path.Combine(_uploadPath, fileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
}