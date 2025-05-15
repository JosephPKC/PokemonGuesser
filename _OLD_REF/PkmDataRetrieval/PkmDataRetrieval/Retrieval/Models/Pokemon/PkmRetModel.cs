namespace PkmDataRetrieval.Retrieval.Models.Pokemon
{
    public class PkmRetModel : BaseRetModel
    {
        public string SpriteFrontDefaultUrl { get; set; } = string.Empty;
        public string SpeciesResUrl { get; set; } = string.Empty;
        public IEnumerable<string> FormResUrls { get; set; } = [];
        public IEnumerable<string> TypeResUrls { get; set; } = [];
        public IEnumerable<PkmAbilityRetModel> Abilities { get; set; } = [];
        public IEnumerable<PkmMoveRetModel> Moves { get; set; } = [];
    }
}
