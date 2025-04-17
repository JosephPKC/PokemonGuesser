namespace PkmDataRetrieval.Retrieval.Models.Form
{
    public class FormRetModel : BaseNamedRetModel
    {
        public string SpriteFrontDefaultUrl { get; set; } = string.Empty;
        public IEnumerable<string> TypeResUrls { get; set; } = [];
    }
}
