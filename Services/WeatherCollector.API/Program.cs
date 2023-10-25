using Microsoft.EntityFrameworkCore;
using WeatherCollector.DAL;
using WeatherCollector.DAL.Context;
using WeatherCollector.DAL.Repositories;
using WeatherCollector.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, opt => opt.MigrationsAssembly("WeatherCollector.DAL.SQLServer")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
builder.Services.AddScoped(typeof(INamedRepository<>), typeof(DbNamedRepository<>));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception exception)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "An error occurred during app initialization.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();