using PkmWebServer.Dtos.Stats;

namespace PkmWebServer.Controllers.Results.Stats;
public class GetStatsResultDto
{
    public StatsDto Stats { get; set; } = new();
}
