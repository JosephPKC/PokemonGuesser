using PkmApi.Dtos.Game.Pokedex;

using Data.Models.Pokedex;
using Data.PkmApi.PkmApiAdapter.Mappers;

namespace Data.PkmApi.PkmApiAdapter.Mappers.DataMappers;
internal class PokedexDataMapper : BaseDataMapper, IDataMapper<PokedexDataModel, PokedexDto>
{
    #region IDataMapper<PokedexDataModel, PokedexDto>
    public PokedexDataModel MapTo(PokedexDto pDto)
    {
        return new()
        {
            Id = pDto.Id,
            NameKey = pDto.Name,
            Names = GetNames(pDto.Names),
            PokemonEntries = GetPokemonEntries(pDto)
        };
    }
    #endregion

    private static IEnumerable<PokedexPkmEntryDataModel> GetPokemonEntries(PokedexDto pDto)
    {
        static bool isValid(PkmEntrySdto pDto)
        {
            return pDto.PokemonSpecies is not null;
        }

        static PokedexPkmEntryDataModel mapTo(PkmEntrySdto pDto)
        {
            return new()
            {
                EntryNumber = pDto.EntryNumber ?? 0,
                ResUrl = GetUrl(pDto.PokemonSpecies)
            };
        }

        return GetLi(pDto.PokemonEntries, isValid, mapTo);
    }
}
