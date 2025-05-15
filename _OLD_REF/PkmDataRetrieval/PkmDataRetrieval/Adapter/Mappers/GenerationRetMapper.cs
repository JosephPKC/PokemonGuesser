using PkmApi.Dtos.Game.Generation;

using PkmDataRetrieval.Retrieval.Models.Generation;

namespace PkmDataRetrieval.Adapter.Mappers
{
    internal static class GenerationRetMapper
    {
        public static GenerationRetModel MapTo(GenerationDto pDto)
        {
            return new()
            {
                Id = pDto.Id,
                NameKey = pDto.Name,
                Names = RetMapperUtils.GetNames(pDto.Names),
                VersionGroupResUrls = RetMapperUtils.GetLi(pDto.VersionGroups)
            };
        }
    }
}
