using PkmDataRetrieval.Retrieval.Models.MoveLearnMethod;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Retrieval.Controllers.StaticData
{
    internal class GetAllMoveLearnMethodsController(IPkmGateway pApi, ICacheHandler pCache, int pCurrGenId)
        : BaseStaticDataController(pApi, pCache, pCurrGenId)
    {
        public IDictionary<string, MoveLearnMethodRetModel>? GetAllMoveLearnMethods()
        {
            return GetAllRetAsDict<MoveLearnMethodRetModel>();
        }
    }
}
