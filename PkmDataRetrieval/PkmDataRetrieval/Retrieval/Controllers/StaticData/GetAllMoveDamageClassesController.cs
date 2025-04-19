using StackExchange.Redis;

using PkmDataRetrieval.Retrieval.Models.MoveDamageClass;

namespace PkmDataRetrieval.Retrieval.Controllers.StaticData
{
    internal class GetAllMoveDamageClassesController(IPkmGateway pApi, IConnectionMultiplexer pConn, string pServiceKeyPrefix, int pCurrGenId)
        : BaseStaticDataController(pApi, pConn, pServiceKeyPrefix, pCurrGenId)
    {
        public IDictionary<string, MoveDamageClassRetModel>? GetAllMoveDamageClasses()
        {
            return GetAllRetAsDict<MoveDamageClassRetModel>();
        }
    }
}
