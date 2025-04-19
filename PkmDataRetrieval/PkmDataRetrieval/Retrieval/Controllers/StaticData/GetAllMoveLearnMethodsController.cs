using StackExchange.Redis;

using PkmDataRetrieval.Retrieval.Models.MoveLearnMethod;

namespace PkmDataRetrieval.Retrieval.Controllers.StaticData
{
    internal class GetAllMoveLearnMethodsController(IPkmGateway pApi, IConnectionMultiplexer pConn, string pServiceKeyPrefix, int pCurrGenId)
        : BaseStaticDataController(pApi, pConn, pServiceKeyPrefix, pCurrGenId)
    {
        public IDictionary<string, MoveLearnMethodRetModel>? GetAllMoveLearnMethods()
        {
            return GetAllRetAsDict<MoveLearnMethodRetModel>();
        }
    }
}
