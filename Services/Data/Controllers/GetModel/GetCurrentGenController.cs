using Data.Models;
using Data.Models.Api;
using Data.Models.Generation;
using Data.PkmApi;
using Data.Utils;

namespace Data.Controllers.GetModel;
internal class GetCurrentGenController(IPkmApiGateway pApi, ICacheHandler pCacheHandler, ILog pLog, int pCurrGenId, ISet<int> pCurrVersGrpIds, string pKeyPrefix, StaticDataLookUp pStaticData)
: BaseGetModelController(pApi, pCacheHandler, pLog, pCurrGenId, pCurrVersGrpIds, pKeyPrefix, pStaticData)
{
    public BasicApiModel? GetCurrentGen()
    {
        string key = $"{_keyPrefix}:{_currGenId}";
        return GetApiModel(key, GetCurrentGenFromApi);
    }

    private BasicApiModel? GetCurrentGenFromApi()
    {
        GenerationDataModel? genModel = GetDataModelById<GenerationDataModel>(_currGenId);
        if (genModel is null)
        {
            return null;
        }

        return new()
        {
            Id = _currGenId,
            Name = new()
            {
                Name = GetEnLangName(genModel.Names) ?? string.Empty,
                NameKey = genModel.NameKey
            }
        };
    }
}
