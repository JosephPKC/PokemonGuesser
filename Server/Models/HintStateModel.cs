namespace Server.Models
{
    public class HintStateModel
    {
        public int Id { get; set; }
        public string HintType { get; set; }
        public string Hint { get; set; }
        public bool IsRevealed { get; set; } = false;
    }
}
