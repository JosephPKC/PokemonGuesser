namespace Data.Models;
public abstract class BaseNamedDataModel : BaseDataModel
{
    public IDictionary<string, string> Names { get; set; } = new Dictionary<string, string>();
}
