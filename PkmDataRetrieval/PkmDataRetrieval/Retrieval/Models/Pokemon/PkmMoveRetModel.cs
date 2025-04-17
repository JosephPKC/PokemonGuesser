namespace PkmDataRetrieval.Retrieval.Models.Pokemon
{
    public class PkmMoveRetModel : BaseResRetModel
    {
        public IEnumerable<PkmMoveVersRetModel> MoveVersions { get; set; } = [];
    }
}
