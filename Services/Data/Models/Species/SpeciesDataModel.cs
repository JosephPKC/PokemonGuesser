namespace Data.Models.Species;
public class SpeciesDataModel : BaseNamedDataModel
{
    public IEnumerable<SpeciesVarietyDataModel> Varieties { get; set; } = [];
}
