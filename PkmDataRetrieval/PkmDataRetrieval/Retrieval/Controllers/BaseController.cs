using StackExchange.Redis;

using PkmDataRetrieval.Api.Models;
using PkmDataRetrieval.Retrieval.Models;
using PkmDataRetrieval.Utils;

namespace PkmDataRetrieval.Retrieval.Controllers
{
    internal abstract class BaseController(IPkmGateway pApi, IConnectionMultiplexer pConn, string pServiceKeyPrefix, int pCurrentGenId)
    {
        protected readonly IPkmGateway _api = pApi;
        protected readonly RedisDbHandler _redis = new(pConn, pServiceKeyPrefix);

        protected readonly int _currGenId = pCurrentGenId;

        #region Get Model
        protected TModel? GetModel<TModel>(string pKey, Func<TModel?> pGetFromApi) where TModel : class, IApiModel
        {
            TModel? model = _redis.Get<TModel>(pKey);
            if (model is not null)
            {
                return model;
            }

            model = pGetFromApi();
            if (model is null)
            {
                //  WARN
                return null;
            }

            _redis.Add(pKey, model);
            return model;
        }

        protected TRet? GetRetById<TRet>(int pId) where TRet : BaseRetModel
        {
            string key = $"{GetRetKeyPrefix<TRet>()}:{pId}";
            TRet? model = _redis.Get<TRet>(key);
            if (model is not null)
            {
                return model;
            }

            model = _api.GetById<TRet>(pId);
            if (model is null)
            {
                //  WARN
                return null;
            }

            _redis.Add(key, model);
            return model;
        }

        protected TRet? GetRetByResUrl<TRet>(string pResUrl) where TRet : BaseRetModel
        {
            int? id = RetrievalUtils.GetIdFromUrl(pResUrl);
            if (id is null)
            {
                //  WARN
                return null;
            }

            TRet? model = GetRetById<TRet>(id.Value);
            if (model is null)
            {
                //  WARN
                return null;
            }

            return model;
        }
        #endregion

        #region Get Key Prefix
        protected static string GetRetKeyPrefix<TRet>() where TRet : BaseRetModel
        {
            return $"{Config.RedisRetKeyPrefix}:{GetRetModelPrefix<TRet>()}";
        }

        protected static string GetRetModelPrefix<TRet>() where TRet : BaseRetModel
        {
            return typeof(TRet) switch
            {
                Type model when model == typeof(Models.Ability.AbilityRetModel) => "ability",
                Type model when model == typeof(Models.Form.FormRetModel) => "form",
                Type model when model == typeof(Models.Generation.GenerationRetModel) => "generation",
                Type model when model == typeof(Models.Move.MoveRetModel) => "move",
                Type model when model == typeof(Models.MoveDamageClass.MoveDamageClassRetModel) => "move-damage-class",
                Type model when model == typeof(Models.MoveLearnMethod.MoveLearnMethodRetModel) => "move-learn-method",
                Type model when model == typeof(Models.Pokedex.PokedexRetModel) => "pokedex",
                Type model when model == typeof(Models.Pokemon.PkmRetModel) => "pokemon",
                Type model when model == typeof(Models.Species.SpeciesRetModel) => "species",
                Type model when model == typeof(Models.Type.TypeRetModel) => "type",
                Type model when model == typeof(Models.VersionGroup.VersionGroupRetModel) => "version-group",
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
