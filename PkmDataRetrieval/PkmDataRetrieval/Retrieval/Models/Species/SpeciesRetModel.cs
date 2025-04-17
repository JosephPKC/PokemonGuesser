namespace PkmDataRetrieval.Retrieval.Models.Species
{
    public class SpeciesRetModel : BaseNamedRetModel
    {
        public IEnumerable<SpeciesVarietyRetModel> Varieties { get; set; } = [];
    }
}
