using StackExchange.Redis;

using PkmApi;
using PkmApi.DTOs.Pokemon;
using PkmApi.DTOs.Shared;

using PkmDataRetrieval.Models;
using PkmDataRetrieval.Mappers;

namespace PkmDataRetrieval.Utils.PkmApiAdapter
{
    /// <summary>
    /// Adapter for the PkmApi library.
    /// It will only expose functions that the DataRetrieval service needs to function.
    /// And, it will transform api data into model data.
    /// </summary>
    internal class PkmApiAdapter(IConnectionMultiplexer pMutex) : IPkmApi
    {
        private readonly PkmApi.IPkmApi _pkmApi = PkmApiFactory.CreatePkmApi();
        private readonly RedisDbHandler _db = new(pMutex, Config.RedisPkmKeyPrefix);

        #region IPkmApi
        public IEnumerable<PkmResModel>? GetAllPkm()
        {
            string key = "all";
            //  Retrieve the models from cache if able
            IEnumerable<PkmResModel>? fromRedis = _db.Get<IEnumerable<PkmResModel>>(key);
            if (fromRedis is not null)
            {
                return fromRedis;
            }

            //  Do an initial api check to get the pkm count.
            ResLiDTO? res = _pkmApi.Pokemon.GetAll();
            if (res is null)
            {
                return null;
            }

            int count = res.Count;

            //  Get all based on the count.
            ResLiDTO? allPkm = _pkmApi.Pokemon.GetAll(count, 0);
            if (allPkm is null)
            {
                return null;
            }

            //  Transform the data into models
            IEnumerable<PkmResModel> allPkmConverted = PkmResModelMapper.MapTo(allPkm);

            //  Cache the models
            bool success = _db.Add(key, allPkmConverted, Config.DefaultRedisPkmKeyLifeInSec);
            if (!success)
            {
                //  Need to warn here.
            }

            return allPkmConverted;
        }

        public PkmModel? GetPkmById(int pId)
        {
            string key = $"{pId}";
            //  Retrieve the models from cache if able
            PkmModel? fromRedis = _db.Get<PkmModel>(key);
            if (fromRedis is not null)
            {
                return fromRedis;
            }
            
            //  Get the data from api
            PkmDTO? res = _pkmApi.Pokemon.GetById(key);
            if (res is null)
            {
                return null;
            }

            //  Transform the data into model
            PkmModel pkm = PkmModelMapper.MapTo(res);

            //  Cache the models
            bool success = _db.Add(key, pkm, Config.DefaultRedisPkmKeyLifeInSec);
            if (!success)
            {
                //  Need to warn here.
            }

            return pkm;
        }
        #endregion
    }
}
