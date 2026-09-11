# Filmklubb – backend

REST-API byggt med ASP.NET WebAPI och SQLite.

Webbapp: https://github.com/JackGriph/filmklubb-frontend

## Krav

- .NET 10 SDK

## Starta

```bash
dotnet run
```

Kör på http://localhost:5071. Databasen skapas och fylls med testdata automatiskt
– inga migrationskommandon behövs.

Swagger: http://localhost:5071/swagger

## Endpoints

| Metod | Route | |
|---|---|---|
| GET | `/api/movies` | lista, `?watched=true` filtrerar |
| GET | `/api/movies/{id}` | enskild film |
| POST | `/api/movies` | skapa |
| PUT | `/api/movies/{id}` | uppdatera, markera sedd, betygsätt |
| POST | `/api/movies/{id}/image` | ladda upp bild |
| DELETE | `/api/movies/{id}` | radera |
