namespace PkmDataRetrieval.Api.Models.Pokemon
{
    public class PkmModel : BasicModel
    {
        public string SpriteUrl { get; set; } = string.Empty;
        public IEnumerable<string> Types { get; set; } = [];
        public IEnumerable<PkmAbilityModel> Abilities { get; set; } = [];
        public IDictionary<string, IEnumerable<PkmMoveModel>> Moves { get; set; } = new Dictionary<string, IEnumerable<PkmMoveModel>>();
        public IEnumerable<PkmOldMoveModel> OldMoves { get; set; } = [];
    }
}
