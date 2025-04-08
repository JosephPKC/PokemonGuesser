namespace PkmDataRetrieval.Models
{
    public class PkmMoveModel
    {
        public required string Name { get; set; }
        public int LevelLearnedAt { get; set; }
        public string LearnMethod { get; set; } = string.Empty;
        public string LatestVersion { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
