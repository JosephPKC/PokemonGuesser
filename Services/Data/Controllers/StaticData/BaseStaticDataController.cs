using Data.Models;
using Data.Models.Basic;
using Data.PkmApi;
using Data.Utils;

namespace Data.Controllers.StaticData;
internal abstract class BaseStaticDataController(IPkmApiGateway pApi, ICacheHandlerFactory pCacheFactory, ILog pLog, int pCurrGenId)
: BaseController(pApi, pCacheFactory, pLog, pCurrGenId)
{
    protected IDictionary<string, TData>? GetDataModelDict<TData>(string pKey, Func<IDictionary<string, TData>?> pGetFromApi) where TData : IDataModel
    {
        IDictionary<string, TData>? model = _cache.Get<IDictionary<string, TData>>(pKey);
        if (model is not null)
        {
            return model;
        }

        model = pGetFromApi();
        if (model is null)
        {
            log.Warn($"Could not get a dict of {nameof(TData)} from key {pKey}.");
            return null;
        }

        _cache.Add(pKey, model);
        return model;
    }

    protected BasicLiDataModel? GetAllDataModel<TData>() where TData : IDataModel
    {
        string key = "all";
        BasicLiDataModel? model = _cache.Get<BasicLiDataModel>(key);
        if (model is not null)
        {
            return model;
        }

        model = _api.GetAll<TData>();
        if (model is null)
        {
            log.Warn($"Could not get a all of {nameof(TData)}.");
            return null;
        }

        _cache.Add(key, model);
        return model;
    }

    protected IDictionary<string, TData>? GetAllDataModelAsDict<TData>() where TData : IDataModel
    {
        BasicLiDataModel? all = GetAllDataModel<TData>();
        if (all is null)
        {
            return null;
        }

        Dictionary<string, TData> dataDict = [];
        foreach (string resUrl in all.Li.Select(x => x.ResUrl))
        {
            TData? retModel = GetDataModelByResUrl<TData>(resUrl);
            if (retModel is null)
            {
                continue;
            }

            if (!dataDict.TryAdd(resUrl, retModel))
            {
                log.Warn($"Trying to add url {resUrl}. Ignoring...");
            }
        }

        return dataDict;
    }
}
