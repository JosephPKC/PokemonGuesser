using StackExchange.Redis;

using PkmDataRetrieval.Retrieval.Models.Type;

namespace PkmDataRetrieval.Retrieval.Controllers.StaticData
{
    internal class GetAllTypesController(IPkmGateway pApi, IConnectionMultiplexer pConn, string pServiceKeyPrefix, int pCurrGenId)
        : BaseStaticDataController(pApi, pConn, pServiceKeyPrefix, pCurrGenId)
    {
        public IDictionary<string, TypeRetModel>? GetAllTypes()
        {
            return GetAllRetAsDict<TypeRetModel>();
        }
    }
}
