using LogWrapper;

using PkmDataRetrieval.Api.Models;
using PkmDataRetrieval.Api.Models.Meta;
using PkmDataRetrieval.Retrieval.Models.Generation;
using PkmDataRetrieval.Retrieval.Models.Meta;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Retrieval.Controllers.GetModel
{
    internal class GetCurrentGenController(IPkmGateway pApi, ICacheHandler pCache, LoggerFactoryConf pLoggerConf, string pKeyPrefix, CurrentIds pCurrentIds, StaticDataCont pStaticData)
        : BaseGetModelController(pApi, pCache, pLoggerConf, pKeyPrefix, pCurrentIds, pStaticData)
    {
        public BasicModel? GetCurrentGen()
        {
            string key = $"{_actionKeyPrefix}:{_currGenId}";
            return GetModel(key, GetCurrentGenFromApi);
        }

        private BasicModel? GetCurrentGenFromApi()
        {
            GenerationRetModel? genRet = GetRetById<GenerationRetModel>(_currGenId);
            if (genRet is null)
            {
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
