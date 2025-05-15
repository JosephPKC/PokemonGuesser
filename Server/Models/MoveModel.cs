namespace Server.Models
{
    public class MoveModel
    {
        public int Id { get; set; }
        public int LevelLearned { get; set; }
        public string Name { get; set; } = string.Empty;
        //public string DamageClass { get; set; } = string.Empty;
        //public string Type {  get; set; } = string.Empty;
        public int Power { get; set; }
        public int Accuracy { get; set; }
        public int Pp { get; set; }
        //public string FlavorText { get; set; } = string.Empty;
        public HintModel DamageClassHint { get; set; } = new();
        public HintModel TypeHint { get; set; } = new();
        public HintModel FlavorTextHint { get; set; } = new();
    }
}
