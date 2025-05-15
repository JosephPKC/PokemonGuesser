namespace Server.Models
{
    public class MoveStateModel
    {
        public MoveModel MoveRef { get; set; } = new();
        public int Id { get; set; }
        public bool IsAnswered { get; set; } = false;
        public string Name { get; set; }
        public HintStateModel DamageClassHint { get; set; } = new();
        public HintStateModel TypeHint { get; set; } = new();
        public HintStateModel FlavorTextHint { get; set; } = new();
        public int Points { get; set; }

    }
}
