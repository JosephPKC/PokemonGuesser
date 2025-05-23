namespace Data.Models.Api.Pokemon;
public class PkmAllApiModel : IApiModel
{
    public IEnumerable<int> Ids { get; set; } = [];
}
