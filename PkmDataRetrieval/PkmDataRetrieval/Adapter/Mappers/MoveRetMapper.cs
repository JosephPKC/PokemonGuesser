using PkmApi.Dtos.Move.Move;

using PkmDataRetrieval.Retrieval.Models.Move;
using PkmDataRetrieval.Retrieval.Models.Shared;

namespace PkmDataRetrieval.Adapter.Mappers
{
    internal static class MoveRetMapper
    {
        public static MoveRetModel MapTo(MoveDto pDto)
        {
            return new()
            {
                Id = pDto.Id,
                NameKey = pDto.Name,
                Accuracy = pDto.Accuracy ?? 0,
                Power = pDto.Power ?? 0,
                Pp = pDto.Pp ?? 0,
                DamageClassResUrl = RetMapperUtils.GetUrl(pDto.DamageClass),
                TypeResUrl = RetMapperUtils.GetUrl(pDto.Type),
                Names = RetMapperUtils.GetNames(pDto.Names),
                FlavorTextEntries = GetFlavorTextEntries(pDto)
            };
        }

        private static IEnumerable<FlavorTextEntryRetModel> GetFlavorTextEntries(MoveDto pDto)
        {
            static bool isValid(MoveFlavorTextSdto pDto)
            {
                return pDto.FlavorText is not null;
            }

            static FlavorTextEntryRetModel mapTo(MoveFlavorTextSdto pDto)
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
