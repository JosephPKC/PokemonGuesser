namespace Server.Models
{
    public class GuessResultModel
    {
        public bool IsCorrect { get; set; } = false;
        public bool IsDuplicate { get; set; } = false;
        public int? MoveId { get; set; }
        public GameStateOutputModel State { get; set; } = new();
    }
}
