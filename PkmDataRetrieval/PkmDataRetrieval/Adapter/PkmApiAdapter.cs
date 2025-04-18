using PkmApi;
using PkmApi.Dtos;
using PkmApi.Dtos.Utility;

using PkmDataRetrieval.Adapter.Mappers;
using PkmDataRetrieval.Retrieval;
using PkmDataRetrieval.Retrieval.Models;
using PkmDataRetrieval.Retrieval.Models.Ability;
using PkmDataRetrieval.Retrieval.Models.Form;
using PkmDataRetrieval.Retrieval.Models.Generation;
using PkmDataRetrieval.Retrieval.Models.Move;
using PkmDataRetrieval.Retrieval.Models.MoveDamageClass;
using PkmDataRetrieval.Retrieval.Models.MoveLearnMethod;
using PkmDataRetrieval.Retrieval.Models.Pokedex;
using PkmDataRetrieval.Retrieval.Models.Pokemon;
using PkmDataRetrieval.Retrieval.Models.Species;
using PkmDataRetrieval.Retrieval.Models.Type;
using PkmDataRetrieval.Retrieval.Models.VersionGroup;


namespace PkmDataRetrieval.Adapter
{
    /// <summary>
    /// A simple adapter to the PkmApi library.
    /// It will get data from the api and turn it into RetModels for the retrieval service.
    /// </summary>
    internal class PkmApiAdapter : IPkmGateway
    {
        private readonly IPkmApi _api = PkmApiFactory.CreatePkmApi();

        #region IPkmGateway
        public IEnumerable<BasicRetModel>? GetAll<TRet>() where TRet : BaseRetModel
        {

            return typeof(TRet) switch
            {
                Type model when model == typeof(AbilityRetModel)         => GetAll(_api.Ability.GetAll),
                Type model when model == typeof(FormRetModel)            => GetAll(_api.Form.GetAll),
                Type model when model == typeof(GenerationRetModel)      => GetAll(_api.Generation.GetAll),
                Type model when model == typeof(MoveRetModel)            => GetAll(_api.Move.GetAll),
                Type model when model == typeof(MoveDamageClassRetModel) => GetAll(_api.MoveDamageClass.GetAll),
                Type model when model == typeof(MoveLearnMethodRetModel) => GetAll(_api.MoveLearnMethod.GetAll),
                Type model when model == typeof(PokedexRetModel)         => GetAll(_api.Pokedex.GetAll),
                Type model when model == typeof(PkmRetModel)             => GetAll(_api.Pokemon.GetAll),
                Type model when model == typeof(SpeciesRetModel)         => GetAll(_api.Species.GetAll),
                Type model when model == typeof(TypeRetModel)            => GetAll(_api.Type.GetAll),
                Type model when model == typeof(VersionGroupRetModel)    => GetAll(_api.VersionGroup.GetAll),
                _ => null
            };
        }

        public TRet? GetById<TRet>(int pId) where TRet : BaseRetModel
        {
            return typeof(TRet) switch
            {
                Type model when model == typeof(AbilityRetModel)         => GetById(pId, _api.Ability.GetById,         AbilityRetMapper.MapTo)         as TRet,
                Type model when model == typeof(FormRetModel)            => GetById(pId, _api.Form.GetById,            FormRetMapper.MapTo)            as TRet,
                Type model when model == typeof(GenerationRetModel)      => GetById(pId, _api.Generation.GetById,      GenerationRetMapper.MapTo)      as TRet,
                Type model when model == typeof(MoveRetModel)            => GetById(pId, _api.Move.GetById,            MoveRetMapper.MapTo)            as TRet,
                Type model when model == typeof(MoveDamageClassRetModel) => GetById(pId, _api.MoveDamageClass.GetById, MoveDamageClassRetMapper.MapTo) as TRet,
                Type model when model == typeof(MoveLearnMethodRetModel) => GetById(pId, _api.MoveLearnMethod.GetById, MoveLearnMethodRetMapper.MapTo) as TRet,
                Type model when model == typeof(PokedexRetModel)         => GetById(pId, _api.Pokedex.GetById,         PokedexRetMapper.MapTo)         as TRet,
                Type model when model == typeof(PkmRetModel)             => GetById(pId, _api.Pokemon.GetById,         PkmRetMapper.MapTo)             as TRet,
                Type model when model == typeof(SpeciesRetModel)         => GetById(pId, _api.Species.GetById,         SpeciesRetMapper.MapTo)         as TRet,
                Type model when model == typeof(TypeRetModel)            => GetById(pId, _api.Type.GetById,            TypeRetMapper.MapTo)            as TRet,
                Type model when model == typeof(VersionGroupRetModel)    => GetById(pId, _api.VersionGroup.GetById,    VersionGroupRetMapper.MapTo)    as TRet,
                _ => null
            };
        }
        #endregion

        private static IEnumerable<BasicRetModel>? GetAll(Func<int, int, ResLiDto?> pGetAll)
        {
            ResLiDto? resInit = pGetAll(1, 0);
            if (resInit is null)
            {
                return null;
            }

            if (resInit.Count is null || resInit.Count == 0)
            {
                return [];
            }

            //  Get all based on the count.
            ResLiDto? res = pGetAll(resInit.Count.Value, 0);
            if (res is null)
            {
                return null;
            }

            //  Transform the data into models
            return BasicRetMapper.MapTo(res);
        }

        private static TRet? GetById<TRet, TDto>(int pId, Func<string, TDto?> pGetById, Func<TDto, TRet> pMapTo) where TRet : BaseRetModel where TDto : IPkmApiDto
        {
            //  Get all based on the count.
            TDto? res = pGetById(pId.ToString());
            if (res is null)
            {
                return null;
            }

            //  Transform the data into models
            return pMapTo(res);
        }
    }
}
