namespace PkmDataRetrieval.Retrieval.Models
{
    public abstract class BaseRetModel
    {
        public int Id { get; set; }
        public string NameKey { get; set; } = string.Empty;
    }
}
