namespace Data.Models.Pokedex;
public class PokedexDataModel : BaseNamedDataModel
{
    public IEnumerable<PokedexPkmEntryDataModel> PokemonEntries { get; set; } = [];
}
