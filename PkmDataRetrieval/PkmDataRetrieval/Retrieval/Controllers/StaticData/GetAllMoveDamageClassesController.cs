using LogWrapper;
using PkmDataRetrieval.Retrieval.Models.MoveDamageClass;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Retrieval.Controllers.StaticData
{
    internal class GetAllMoveDamageClassesController(IPkmGateway pApi, ICacheHandler pCache, LoggerFactoryConf pLoggerConf, int pCurrGenId)
        : BaseStaticDataController(pApi, pCache, pLoggerConf, pCurrGenId)
    {
        public IDictionary<string, MoveDamageClassRetModel>? GetAllMoveDamageClasses()
        {
            return GetAllRetAsDict<MoveDamageClassRetModel>();
        }
    }
}
