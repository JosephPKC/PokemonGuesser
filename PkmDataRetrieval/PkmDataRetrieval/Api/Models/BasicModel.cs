using PkmDataRetrieval.Api.Models.Shared;

namespace PkmDataRetrieval.Api.Models
{
    public class BasicModel : IApiModel
    {
        public int Id { get; set; }
        public NameModel Name { get; set; } = new();
    }
}
