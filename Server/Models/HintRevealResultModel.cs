namespace Server.Models
{
    public class HintRevealResultModel
    {
        public bool IsAlreadyRevealed { get; set; } = false;
        public bool IsMoveAlreadyAnswered { get; set; } = false;
        public GameStateOutputModel State { get; set; } = new();
    }
}
