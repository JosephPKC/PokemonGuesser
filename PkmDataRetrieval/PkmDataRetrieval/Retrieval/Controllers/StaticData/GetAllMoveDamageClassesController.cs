using PkmDataRetrieval.Retrieval.Models.MoveDamageClass;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Retrieval.Controllers.StaticData
{
    internal class GetAllMoveDamageClassesController(IPkmGateway pApi, ICacheHandler pCache, int pCurrGenId)
        : BaseStaticDataController(pApi, pCache, pCurrGenId)
    {
        public IDictionary<string, MoveDamageClassRetModel>? GetAllMoveDamageClasses()
        {
            return GetAllRetAsDict<MoveDamageClassRetModel>();
        }
    }
}
