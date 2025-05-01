using PkmDataRetrieval.Retrieval.Models.Type;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Retrieval.Controllers.StaticData
{
    internal class GetAllTypesController(IPkmGateway pApi, ICacheHandler pCache, int pCurrGenId)
        : BaseStaticDataController(pApi, pCache, pCurrGenId)
    {
        public IDictionary<string, TypeRetModel>? GetAllTypes()
        {
            return GetAllRetAsDict<TypeRetModel>();
        }
    }
}
