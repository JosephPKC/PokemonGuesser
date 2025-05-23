using LogWrapper;
using PkmDataRetrieval.Retrieval.Models.MoveLearnMethod;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Retrieval.Controllers.StaticData
{
    internal class GetAllMoveLearnMethodsController(IPkmGateway pApi, ICacheHandler pCache, LoggerFactoryConf pLoggerConf, int pCurrGenId)
        : BaseStaticDataController(pApi, pCache, pLoggerConf, pCurrGenId)
    {
        public IDictionary<string, MoveLearnMethodRetModel>? GetAllMoveLearnMethods()
        {
            return GetAllRetAsDict<MoveLearnMethodRetModel>();
        }
    }
}
