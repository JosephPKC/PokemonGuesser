using PkmDataRetrieval.Retrieval.Shared;

namespace PkmDataRetrieval.Retrieval.Models.Ability
{
    public class AbilityRetModel : BaseNamedRetModel
    {
        public IEnumerable<FlavorTextEntryRetModel> FlavorTextEntries { get; set; } = [];
    }
}
