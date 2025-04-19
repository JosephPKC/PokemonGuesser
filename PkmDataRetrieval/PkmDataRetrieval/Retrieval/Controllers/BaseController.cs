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
