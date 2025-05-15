namespace Data.Models.Pokemon;
public class PkmDataModel : BaseDataModel
{
    public string SpriteFrontDefaultUrl { get; set; } = string.Empty;
    public string SpeciesResUrl { get; set; } = string.Empty;
    public IEnumerable<string> FormResUrls { get; set; } = [];
    public IEnumerable<string> TypeResUrls { get; set; } = [];
    public IEnumerable<PkmMoveDataModel> Moves { get; set; } = [];
}
