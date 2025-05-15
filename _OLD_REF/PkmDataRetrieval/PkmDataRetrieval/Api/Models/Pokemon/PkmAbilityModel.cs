namespace PkmDataRetrieval.Api.Models.Pokemon
{
    public class PkmAbilityModel : BasicModel
    {
        public string FlavorText { get; set; } = string.Empty;
        public bool IsHidden { get; set; } = false;
        public int Order { get; set; }
    }
}
