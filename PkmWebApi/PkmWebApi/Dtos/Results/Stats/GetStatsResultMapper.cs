using PkmWebApi.Dtos.Stats;
using PkmWebApi.Models.States;

namespace PkmWebApi.Dtos.Results.Stats;
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
