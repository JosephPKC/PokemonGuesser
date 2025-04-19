using PkmApi.Dtos.Game.VersionGroup;

using PkmDataRetrieval.Retrieval.Models.VersionGroup;

namespace PkmDataRetrieval.Adapter.Mappers
{
    internal static class VersionGroupRetMapper
    {
        public static VersionGroupRetModel MapTo(VersionGroupDto pDto)
        {
            return new()
            {
                Id = pDto.Id,
                NameKey = pDto.Name,
                PokedexResUrls = RetMapperUtils.GetLi(pDto.Pokedexes)
            };
        }
    }
}
