namespace PkmDataRetrieval.Retrieval.Models
{
    public abstract class BaseNamedRetModel : BaseRetModel
    {
        public IDictionary<string, string> Names { get; set; } = new Dictionary<string, string>();
    }
}
