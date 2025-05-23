namespace Data.Models.Form;
public class FormDataModel : BaseNamedDataModel
{
    public string SpriteFrontDefaultUrl { get; set; } = string.Empty;
    public IEnumerable<string> TypeResUrls { get; set; } = [];
}
