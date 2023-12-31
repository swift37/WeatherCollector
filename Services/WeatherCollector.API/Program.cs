using Microsoft.EntityFrameworkCore;
using Serilog;
using WeatherCollector.DAL;
using WeatherCollector.DAL.Context;
using WeatherCollector.DAL.Repositories;
using WeatherCollector.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));

var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, opt => opt.MigrationsAssembly("WeatherCollector.DAL.SQLServer")));

builder.Services.AddScoped(typeof(DataValuesRepository));
builder.Services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
builder.Services.AddScoped(typeof(INamedRepository<>), typeof(DbNamedRepository<>));

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

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
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseHttpsRedirection();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();