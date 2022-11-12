using Microsoft.EntityFrameworkCore;

namespace NpgDataSourceRepro;

public class WeatherContext : DbContext
{
    public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
    {
        
    }

    public DbSet<Forecast> Forecasts { get; set; } = null!;
}