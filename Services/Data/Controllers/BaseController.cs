using Data.Models;
using Data.Models.Api;
using Data.PkmApi;
using Data.Utils;

namespace Data.Controllers;
internal abstract class BaseController(IPkmApiGateway pApi, ICacheHandler pCacheHandler, ILog pLog, int pCurrentGenId)
{
    protected readonly IPkmApiGateway _api = pApi;
    protected readonly ICacheHandler _cache = pCacheHandler;
    protected readonly ILog log = pLog;

    protected readonly int _currGenId = pCurrentGenId;

    protected TModel? GetApiModel<TModel>(string pKey, Func<TModel?> pGetFromApi) where TModel : class, IApiModel
    {
        TModel? model = _cache.Get<TModel>(pKey);
        if (model is not null)
        {
            return model;
        }

        model = pGetFromApi();
        if (model is null)
        {
            log.Warn($"Could not get {nameof(TModel)} from api.");
            return null;
        }

        _cache.Add(pKey, model);
        return model;
    }

    protected TModel? GetDataModelById<TModel>(int pId) where TModel : class, IDataModel
    {
        TModel? model = _cache.Get<TModel>(pId.ToString());
        if (model is not null)
        {
            return model;
        }

        model = _api.GetById<TModel>(pId);
        if (model is null)
        {
            log.Warn($"Could not get {nameof(TModel)} with id {pId}.");
            return null;
        }

        _cache.Add(pId.ToString(), model);
        return model;
    }

    protected TModel? GetDataModelByResUrl<TModel>(string pResUrl) where TModel : class, IDataModel
    {
        int? id = DataUrlUtils.GetIdFromUrl(pResUrl);
        if (id is null)
        {
            log.Warn($"Could not get an id from url {pResUrl} for {nameof(TModel)}.");
            return null;
        }

        TModel? model = GetDataModelById<TModel>(id.Value);
        if (model is null)
        {
            log.Warn($"Could not get {nameof(TModel)} with id {id.Value}.");
            return null;
        }
         
        return model;
    }

    protected static string? GetEnLangName(IDictionary<string, string> pNames)
    {
        string pEnLangUrl = DataUrlUtils.GetUrlFromId(Config.EngLangId, Config.LangResName);
        if (pNames.TryGetValue(pEnLangUrl, out string? value))
        {
            return value;
        }

        return null;
    }
}
