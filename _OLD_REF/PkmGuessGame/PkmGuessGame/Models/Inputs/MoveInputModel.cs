namespace PkmGuessGame.Models.Inputs
{
    public class MoveInputModel : BaseInputModel
    {
        public string DamageClass { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string FlavorText { get; set; } = string.Empty;
    }
}
