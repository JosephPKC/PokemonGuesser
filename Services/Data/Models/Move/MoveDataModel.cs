namespace Data.Models.Move;

public class MoveDataModel : BaseNamedDataModel
{
    public string DamageClassResUrl { get; set; } = string.Empty;
    public string TypeResUrl { get; set; } = string.Empty;
    public IEnumerable<FlavorTextEntryDataModel> FlavorTextEntries { get; set; } = [];
    public int Accuracy { get; set; }
    public int Power { get; set; }
    public int Pp { get; set; }
}
