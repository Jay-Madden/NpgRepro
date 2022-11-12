using NodaTime;

namespace NpgDataSourceRepro;

public class Forecast 
{
    public int Id { get; set; }
    
    public int Temperature { get; set; }
    
    public LocalDateTime Time { get; set; }
}