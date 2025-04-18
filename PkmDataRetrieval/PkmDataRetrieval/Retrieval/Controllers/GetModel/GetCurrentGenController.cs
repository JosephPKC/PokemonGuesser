using StackExchange.Redis;

using PkmDataRetrieval.Api.Models.Meta;
using PkmDataRetrieval.Api.Models.Generation;
using PkmDataRetrieval.Retrieval.Models.Generation;
using PkmDataRetrieval.Retrieval.Models.Meta;

namespace PkmDataRetrieval.Retrieval.Controllers.GetModel
{
    internal class GetCurrentGenController(IPkmGateway pApi, IConnectionMultiplexer pConn, KeyPrefixes pKeyPrefixes, CurrentIds pCurrentIds, StaticDataCont pStaticData)
        : BaseGetModelController(pApi, pConn, pKeyPrefixes, pCurrentIds, pStaticData)
    {
        public GenModel? GetCurrentGen()
        {
            string key = $"{_actionKeyPrefix}:{_currGenId}";
            return GetModel(key, GetCurrentGenFromApi);
        }

        private GenModel? GetCurrentGenFromApi()
        {
            GenerationRetModel? genRet = GetRetById<GenerationRetModel>(_currGenId);
            if (genRet is null)
            {
                //  WARN
                return null;
            }

            return new()
            {
                Id = _currGenId,
                Name = new()
                {
                    Name = GetEnLangName(genRet.Names) ?? string.Empty,
                    NameKey = genRet.NameKey
                }
            };
        }
    }
}
