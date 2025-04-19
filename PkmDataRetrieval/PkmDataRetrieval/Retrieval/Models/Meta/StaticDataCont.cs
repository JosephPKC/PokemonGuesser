using PkmDataRetrieval.Retrieval.Models.MoveDamageClass;
using PkmDataRetrieval.Retrieval.Models.MoveLearnMethod;
using PkmDataRetrieval.Retrieval.Models.Type;

namespace PkmDataRetrieval.Retrieval.Models.Meta
{
    internal class StaticDataCont
    {
        public IDictionary<string, MoveDamageClassRetModel> MoveDamageClasses { get; set; } = new Dictionary<string, MoveDamageClassRetModel>();
        public IDictionary<string, MoveLearnMethodRetModel> MoveLearnMethods { get; set; } = new Dictionary<string, MoveLearnMethodRetModel>();
        public IDictionary<string, TypeRetModel> Types { get; set; } = new Dictionary<string, TypeRetModel>();
    }
}
