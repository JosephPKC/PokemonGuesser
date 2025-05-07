namespace PkmGuessGame.Models.Inputs
{
    public class MoveInputModel
    {
        public string Name { get; set; } = string.Empty;
        public int Id { get; set; }
        public string DamageClass { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string FlavorText { get; set; } = string.Empty;
    }
}
