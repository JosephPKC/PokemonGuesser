using PkmApi.Dtos.Game.Pokedex;

using PkmDataRetrieval.Retrieval.Models.Pokedex;

namespace PkmDataRetrieval.Adapter.Mappers
{
    internal static class PokedexRetMapper
    {
        public static PokedexRetModel MapTo(PokedexDto pDto)
        {
            return new()
            {
                Id = pDto.Id,
                NameKey = pDto.Name,
                Names = RetMapperUtils.GetNames(pDto.Names),
                PokemonEntries = GetPokemonEntries(pDto)
            };
        }

        private static IEnumerable<PokedexPkmEntryRetModel> GetPokemonEntries(PokedexDto pDto)
        {
            static bool isValid(PkmEntrySdto pDto)
            {
                return pDto.PokemonSpecies is not null;
            }

            static PokedexPkmEntryRetModel mapTo(PkmEntrySdto pDto)
            {
                return new()
                {
                    EntryNumber = pDto.EntryNumber ?? 0,
                    ResUrl = RetMapperUtils.GetUrl(pDto.PokemonSpecies)
                };
            }

            return RetMapperUtils.GetLi(pDto.PokemonEntries, isValid, mapTo);
        }
    }
}
