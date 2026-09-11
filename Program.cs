using System.Text.Json.Serialization;
using filmklubb_backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers. JsonStringEnumConverter gör att API:et svarar
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


builder.Services.AddOpenApi();

builder.Services.AddDbContext<FilmklubbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS för Vites dev-server.
const string DevCors = "DevCors";
builder.Services.AddCors(options =>
    options.AddPolicy(DevCors, policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()));


var app = builder.Build();

// Kör väntande migrationer vid uppstart. Gör att den som klonar repot
// bara behöver "dotnet run" - databasen skapas och seedas automatiskt.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FilmklubbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/openapi/v1.json", "Filmklubb API v1"));
}
else
{
    // Bara utanför Development: i dev anropar React http://localhost:5071,
    // och en redirect till https med självsignerat cert får fetch att fela.
    app.UseHttpsRedirection();
}

app.UseCors(DevCors);

// Serverar wwwroot/ så att uppladdade bilder nås via /uploads/<filnamn>
app.UseStaticFiles();

app.MapControllers();

app.Run();

