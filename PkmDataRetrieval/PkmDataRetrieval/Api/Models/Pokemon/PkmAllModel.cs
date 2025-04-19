namespace PkmDataRetrieval.Api.Models.Pokemon
{
    public class PkmAllModel : IApiModel
    {
        public IEnumerable<int> Ids { get; set; } = [];
    }
}
