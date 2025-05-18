using PkmWebServer.Dtos.Stats;
using PkmWebServer.Models.States;

namespace PkmWebServer.Controllers.Results.Stats;
public static class GetStatsResultMapper
{
    public static GetStatsResultDto GetResult(StatsModel pState)
    {
        return new()
        {
            Stats = StatsDtoMapper.MapToDto(pState)
        };
    }
}
