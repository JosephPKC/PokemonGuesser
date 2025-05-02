using LogWrapper;
using PkmDataRetrieval.Retrieval.Models.Type;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Retrieval.Controllers.StaticData
{
    internal class GetAllTypesController(IPkmGateway pApi, ICacheHandler pCache, LoggerFactoryConf pLoggerConf, int pCurrGenId)
        : BaseStaticDataController(pApi, pCache, pLoggerConf, pCurrGenId)
    {
        public IDictionary<string, TypeRetModel>? GetAllTypes()
        {
            return GetAllRetAsDict<TypeRetModel>();
        }
    }
}
