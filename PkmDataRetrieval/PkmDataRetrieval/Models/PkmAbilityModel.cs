namespace PkmDataRetrieval.Models
{
    public class PkmAbilityModel
    {
        public required string Name { get; set; }
        public bool IsHidden { get; set; } = false;
        public int Slot { get; set; }
    }
}
