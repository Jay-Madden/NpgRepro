using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NpgDataSourceRepro.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherContext _context;
    private readonly ILogger<WeatherForecastController> _logger;
    
    public WeatherForecastController(WeatherContext context, ILogger<WeatherForecastController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public int Get()
    {
        _logger.LogInformation("Connection String in context: {ConnectionString}", _context.Database.GetConnectionString());
        
        // This call will fail
        var foo = _context.Forecasts.ToList();
        
        return 1;
    }
}