using PkmWebApi.Dtos.Stats;

namespace PkmWebApi.Dtos.Results.Stats;
public class GetStatsResultDto
{
    public StatsDto Stats { get; set; } = new();
}
