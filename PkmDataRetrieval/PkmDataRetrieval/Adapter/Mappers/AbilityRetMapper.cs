using PkmApi.Dtos.Pokemon.Ability;

using PkmDataRetrieval.Retrieval.Models.Ability;
using PkmDataRetrieval.Retrieval.Models.Shared;

namespace PkmDataRetrieval.Adapter.Mappers
{
    internal static class AbilityRetMapper
    {
        public static AbilityRetModel MapTo(AbilityDto pDto)
        {
            return new()
            {
                Id = pDto.Id,
                NameKey = pDto.Name,
                Names = RetMapperUtils.GetNames(pDto.Names),
                FlavorTextEntries = GetFlavorTextEntries(pDto)
            };
        }

        private static IEnumerable<FlavorTextEntryRetModel> GetFlavorTextEntries(AbilityDto pDto)
        {
            static bool isValid(AbilityFlavorTextSdto pDto)
            {
                return pDto.FlavorText is not null;
            }

            static FlavorTextEntryRetModel mapTo(AbilityFlavorTextSdto pDto)
            {
                return new()
                {
                    FlavorTextEntry = pDto.FlavorText ?? string.Empty,
                    LanguageResUrl = RetMapperUtils.GetUrl(pDto.Language),
                    VersionGroupResUrl = RetMapperUtils.GetUrl(pDto.VersionGroup)
                };
            }

            return RetMapperUtils.GetLi(pDto.FlavorTextEntries, isValid, mapTo);
        }
    }
}
