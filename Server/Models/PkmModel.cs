namespace Server.Models
{
    public class PkmModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public PkmType Types { get; set; } = new();
        public IEnumerable<MoveModel> Moves { get; set; } = [];
    }

    public class PkmType
    {
        public string Type1 { get; set; } = string.Empty;
        public string Type2 { get; set; } = string.Empty;
    }
}
