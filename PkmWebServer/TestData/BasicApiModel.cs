namespace PkmWebServer.TestData;
public class BasicApiModel : IApiModel
{
    public int Id { get; set; }
    public NameApiModel Name { get; set; } = new();
}
