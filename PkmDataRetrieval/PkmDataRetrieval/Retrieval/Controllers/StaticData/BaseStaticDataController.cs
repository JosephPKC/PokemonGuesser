using StackExchange.Redis;

using PkmDataRetrieval.Retrieval.Models;

namespace PkmDataRetrieval.Retrieval.Controllers.StaticData
{
    internal abstract class BaseStaticDataController(IPkmGateway pApi, IConnectionMultiplexer pConn, string pServiceKeyPrefix, int pCurrGenId)
        : BaseController(pApi, pConn, pServiceKeyPrefix, pCurrGenId)
    {
        protected IDictionary<string, TRet>? GetRetDict<TRet>(string pKey, Func<IDictionary<string, TRet>?> pGetFromApi) where TRet : BaseRetModel
        {
            IDictionary<string, TRet>? model = _redis.Get<IDictionary<string, TRet>>(pKey);
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

        protected IEnumerable<BasicRetModel>? GetAllRet<TRet>() where TRet : BaseRetModel
        {
            string key = $"{GetRetKeyPrefix<TRet>()}:all";
            IEnumerable<BasicRetModel>? model = _redis.Get<IEnumerable<BasicRetModel>>(key);
            if (model is not null)
            {
                return model;
            }

            model = _api.GetAll<TRet>();
            if (model is null)
            {
                //  WARN
                return null;
            }

            _redis.Add(key, model);
            return model;
        }

        protected IDictionary<string, TRet>? GetAllRetAsDict<TRet>() where TRet : BaseRetModel
        {
            IEnumerable<BasicRetModel>? allRets = GetAllRet<TRet>();
            if (allRets is null)
            {
                //  WARN
                return null;
            }

            IDictionary<string, TRet> retDict = new Dictionary<string, TRet>();
            foreach (BasicRetModel basicRet in allRets)
            {
                TRet? retModel = GetRetByResUrl<TRet>(basicRet.ResUrl);
                if (retModel is null)
                {
                    //  WARN
                    continue;
                }

                if (!retDict.TryAdd(basicRet.ResUrl, retModel))
                {
                    //  WARN
                }
            }

            return retDict;
        }
    }
}
