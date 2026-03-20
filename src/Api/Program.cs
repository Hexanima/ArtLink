using Api.Data;
using Api.Services;
using Api.Mappers;
using Domain.Entities;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Application.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Agregar controladores
builder.Services.AddControllers();

// Registrar mappers
builder.Services.AddScoped<IMapper<ArtworkDTO, Artwork>, ArtworkMapper>();
builder.Services.AddScoped<IMapper<ServiceDTO, Service>, ServiceMapper>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()      // Permitir cualquier origen
              .AllowAnyMethod()      // Permitir cualquier método (GET, POST, etc)
              .AllowAnyHeader();     // Permitir cualquier header
    });
    
    // Opcional: Política más restrictiva para producción
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=Data/Data.db"));

builder.Services.AddScoped<IService<Artwork>, PostgreArtworkService>();
builder.Services.AddScoped<IService<Service>, PostgreServiceService>();


builder.Services.AddScoped(typeof(IService<>), typeof(PostgreService<>));

// Registrar casos de uso genéricos - Application.UseCases.Base.Interfaces/Implementation
builder.Services.AddScoped(typeof(Application.UseCases.Base.Interfaces.IGetAllUseCase<>), typeof(Application.UseCases.Base.Implementation.GetAllUseCase<>));
builder.Services.AddScoped(typeof(Application.UseCases.Base.Interfaces.IGetByIdUseCase<>), typeof(Application.UseCases.Base.Implementation.GetByIdUseCase<>));
builder.Services.AddScoped(typeof(Application.UseCases.Base.Interfaces.ICreateUseCase<>), typeof(Application.UseCases.Base.Implementation.CreateUseCase<>));
builder.Services.AddScoped(typeof(Application.UseCases.Base.Interfaces.IUpdateUseCase<>), typeof(Application.UseCases.Base.Implementation.UpdateUseCase<>));
builder.Services.AddScoped(typeof(Application.UseCases.Base.Interfaces.IDeleteUseCase<>), typeof(Application.UseCases.Base.Implementation.DeleteUseCase<>));




var app = builder.Build();

// Aplicar CORS
app.UseCors("AllowAll");  // En desarrollo usamos AllowAll, en producción cambiar a "AllowFrontend"

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
