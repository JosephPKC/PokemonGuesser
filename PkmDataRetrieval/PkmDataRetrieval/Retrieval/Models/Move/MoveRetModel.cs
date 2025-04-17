using PkmDataRetrieval.Retrieval.Shared;

namespace PkmDataRetrieval.Retrieval.Models.Move
{
    public class MoveRetModel : BaseNamedRetModel
    {
        public string DamageClassResUrl { get; set; } = string.Empty;
        public IEnumerable<FlavorTextEntryRetModel> FlavorTextEntries { get; set; } = [];
        public int Accuracy { get; set; }
        public int Power { get; set; }
        public int Pp { get; set; }
        public string TypeResUrl { get; set; } = string.Empty;
    }
}
