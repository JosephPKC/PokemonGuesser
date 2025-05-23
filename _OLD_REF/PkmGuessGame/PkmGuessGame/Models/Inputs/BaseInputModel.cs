namespace PkmGuessGame.Models.Inputs
{
    public abstract class BaseInputModel
    {
        public string Name { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
