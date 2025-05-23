namespace PkmDataRetrieval.Retrieval.Models.Pokedex
{
    public class PokedexRetModel : BaseNamedRetModel
    {
        public IEnumerable<PokedexPkmEntryRetModel> PokemonEntries { get; set; } = [];
    }
}
