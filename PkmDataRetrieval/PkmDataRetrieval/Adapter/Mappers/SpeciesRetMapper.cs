using PkmApi.Dtos.Pokemon.Species;

using PkmDataRetrieval.Retrieval.Models.Species;

namespace PkmDataRetrieval.Adapter.Mappers
{
    internal static class SpeciesRetMapper
    {
        public static SpeciesRetModel MapTo(SpeciesDto pDto)
        {
            return new()
            {
                Id = pDto.Id,
                NameKey = pDto.Name,
                Names = RetMapperUtils.GetNames(pDto.Names),
                Varieties = GetPokemonEntries(pDto) 
            };
        }

        private static IEnumerable<SpeciesVarietyRetModel> GetPokemonEntries(SpeciesDto pDto)
        {
            static bool isValid(PkmSpeciesVarietySdto pDto)
            {
                return pDto.Pokemon is not null;
            }

            static SpeciesVarietyRetModel mapTo(PkmSpeciesVarietySdto pDto)
            {
                return new()
                {
                    IsDefault = pDto.IsDefault ?? false,
                    NameKey = pDto.Pokemon is not null ? pDto.Pokemon.Name : string.Empty,
                    ResUrl = RetMapperUtils.GetUrl(pDto.Pokemon)
                };
            }

            return RetMapperUtils.GetLi(pDto.Varieties, isValid, mapTo);
        }
    }
}
