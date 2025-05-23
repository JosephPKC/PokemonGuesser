namespace PkmDataRetrieval.Api.Models.Meta
{
    public class CurrentIds
    {
        public int CurrentGenId { get; set; }
        public ISet<int> CurrentVersGrpIds { get; set; } = new HashSet<int>();
    }
}
