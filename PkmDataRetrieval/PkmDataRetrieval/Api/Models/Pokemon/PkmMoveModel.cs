namespace PkmDataRetrieval.Api.Models.Pokemon
{
    public class PkmMoveModel : BasicModel
    {
        public string MoveType { get; set; } = string.Empty;
        public string DamageClass { get; set; } = string.Empty;
        public string LearnMethod { get; set; } = string.Empty;
        public int? LevelLearned { get; set; }
        public int Accuracy { get; set; }
        public int Power { get; set; }
        public int Pp { get; set; }
        public string FlavorText { get; set; } = string.Empty;
        public int Order { get; set; }
    }
}
