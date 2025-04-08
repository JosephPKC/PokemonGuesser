namespace PkmDataRetrieval.Models
{
    public class PkmModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string Species { get; set; } = string.Empty;
        public IEnumerable<PkmAbilityModel> Abilities { get; set; } = [];
        public IEnumerable<PkmMoveModel> Moves { get; set; } = [];
        public IEnumerable<PkmStatModel> Stats { get; set; } = [];
        public IEnumerable<string> Types { get; set; } = [];
        public string SpriteUrl { get; set; } = string.Empty;
    }
}
