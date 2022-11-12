using Microsoft.EntityFrameworkCore;
using NpgDataSourceRepro;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dataSourceBuilder = new NpgsqlDataSourceBuilder("**InsertConnectionString**");
dataSourceBuilder.UseNodaTime();
var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<WeatherContext>(o =>
    o.UseNpgsql(dataSource, optionsBuilder => optionsBuilder.UseNodaTime()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

// ****** Create the EF Context scope and inject ClemBotContext
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<WeatherContext>();
// ******

// These database call will work, the ones in WeatherForecastController will not
await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();

var foo = context.Forecasts.ToList();
// ---------

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();