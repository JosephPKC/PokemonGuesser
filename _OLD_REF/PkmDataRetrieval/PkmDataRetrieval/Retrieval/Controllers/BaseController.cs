using LogWrapper;

using PkmDataRetrieval.Api.Models;
using PkmDataRetrieval.Retrieval.Models;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Retrieval.Controllers
{
    internal abstract class BaseController(IPkmGateway pApi, ICacheHandler pCache, LoggerFactoryConf pLoggerConf, int pCurrentGenId)
    {
        protected readonly IPkmGateway _api = pApi;
        protected readonly ICacheHandler _cache = pCache;
        protected readonly LogWrapper.Loggers.ILogger log = pLoggerConf.LoggerFactory.CreateNewLogger(pLoggerConf.DeclaringType, pLoggerConf.LogLevel);

        protected readonly int _currGenId = pCurrentGenId;

        #region Get Model
        protected TModel? GetModel<TModel>(string pKey, Func<TModel?> pGetFromApi) where TModel : class, IApiModel
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

        protected TRet? GetRetById<TRet>(int pId) where TRet : BaseRetModel
        {
            string key = $"{GetRetKeyPrefix<TRet>()}:{pId}";
            TRet? model = _cache.Get<TRet>(key);
            if (model is not null)
            {
                return model;
            }

            model = _api.GetById<TRet>(pId);
            if (model is null)
            {
                log.Warn($"Could not get {nameof(TRet)} with id {pId}.");
                return null;
            }

            _cache.Add(key, model);
            return model;
        }

        protected TRet? GetRetByResUrl<TRet>(string pResUrl) where TRet : BaseRetModel
        {
            int? id = RetrievalUtils.GetIdFromUrl(pResUrl);
            if (id is null)
            {
                log.Warn($"Could not get an id from url {pResUrl} for {nameof(TRet)}.");
                return null;
            }

            TRet? model = GetRetById<TRet>(id.Value);
            if (model is null)
            {
                log.Warn($"Could not get {nameof(TRet)} with id {id.Value}.");
                return null;
            }
             
            return model;
        }
        #endregion

        #region Get Key Prefix
        protected static string GetRetKeyPrefix<TRet>() where TRet : BaseRetModel
        {
            return $"{Config.RetKeyPrefix}:{GetRetModelPrefix<TRet>()}";
        }

        protected static string GetRetModelPrefix<TRet>() where TRet : BaseRetModel
        {
            return typeof(TRet).Name switch
            {
                nameof(Models.Ability.AbilityRetModel) => "ability",
                nameof(Models.Form.FormRetModel) => "form",
                nameof(Models.Generation.GenerationRetModel) => "generation",
                nameof(Models.Move.MoveRetModel) => "move",
                nameof(Models.MoveDamageClass.MoveDamageClassRetModel) => "move-damage-class",
                nameof(Models.MoveLearnMethod.MoveLearnMethodRetModel) => "move-learn-method",
                nameof(Models.Pokedex.PokedexRetModel) => "pokedex",
                nameof(Models.Pokemon.PkmRetModel) => "pokemon",
                nameof(Models.Species.SpeciesRetModel) => "species",
                nameof(Models.Type.TypeRetModel) => "type",
                nameof(Models.VersionGroup.VersionGroupRetModel) => "version-group",
                _ => string.Empty
            };
        }
        #endregion

        protected static string? GetEnLangName(IDictionary<string, string> pNames)
        {
            string pEnLangUrl = RetrievalUtils.GetUrlFromId(Config.EngLangId, Config.LangResName);
            if (pNames.TryGetValue(pEnLangUrl, out string? value))
            {
                return value;
            }

            return null;
        }
    }
}
