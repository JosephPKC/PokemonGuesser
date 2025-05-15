namespace Data.Models;
public abstract class BaseDataModel : IDataModel
{
    public int Id { get; set; }
    public string NameKey { get; set; } = string.Empty;
}
