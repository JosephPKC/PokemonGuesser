using PkmDataRetrieval.Api.Models.Shared;

namespace PkmDataRetrieval.Api.Models.Pokemon
{
    public class PkmMoveModel : BasicModel
    {
        public NameModel MoveType { get; set; } = new();
        public NameModel DamageClass { get; set; } = new();
        public NameModel LearnMethod { get; set; } = new();
        public int? LevelLearned { get; set; }
        public int Accuracy { get; set; }
        public int Power { get; set; }
        public int Pp { get; set; }
        public string FlavorText { get; set; } = string.Empty;
    }
}
