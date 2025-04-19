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
            return typeof(TRet).Name switch
            {
                nameof(AbilityRetModel)         => GetAll(_api.Ability.GetAll),
                nameof(FormRetModel)            => GetAll(_api.Form.GetAll),
                nameof(GenerationRetModel)      => GetAll(_api.Generation.GetAll),
                nameof(MoveRetModel)            => GetAll(_api.Move.GetAll),
                nameof(MoveDamageClassRetModel) => GetAll(_api.MoveDamageClass.GetAll),
                nameof(MoveLearnMethodRetModel) => GetAll(_api.MoveLearnMethod.GetAll),
                nameof(PokedexRetModel)         => GetAll(_api.Pokedex.GetAll),
                nameof(PkmRetModel)             => GetAll(_api.Pokemon.GetAll),
                nameof(SpeciesRetModel)         => GetAll(_api.Species.GetAll),
                nameof(TypeRetModel)            => GetAll(_api.Type.GetAll),
                nameof(VersionGroupRetModel)    => GetAll(_api.VersionGroup.GetAll),
                _ => null
            };
        }

        public TRet? GetById<TRet>(int pId) where TRet : BaseRetModel
        {
            return typeof(TRet).Name switch
            {
                nameof(AbilityRetModel)         => GetById(pId, _api.Ability.GetById,         AbilityRetMapper.MapTo)         as TRet,
                nameof(FormRetModel)            => GetById(pId, _api.Form.GetById,            FormRetMapper.MapTo)            as TRet,
                nameof(GenerationRetModel)      => GetById(pId, _api.Generation.GetById,      GenerationRetMapper.MapTo)      as TRet,
                nameof(MoveRetModel)            => GetById(pId, _api.Move.GetById,            MoveRetMapper.MapTo)            as TRet,
                nameof(MoveDamageClassRetModel) => GetById(pId, _api.MoveDamageClass.GetById, MoveDamageClassRetMapper.MapTo) as TRet,
                nameof(MoveLearnMethodRetModel) => GetById(pId, _api.MoveLearnMethod.GetById, MoveLearnMethodRetMapper.MapTo) as TRet,
                nameof(PokedexRetModel)         => GetById(pId, _api.Pokedex.GetById,         PokedexRetMapper.MapTo)         as TRet,
                nameof(PkmRetModel)             => GetById(pId, _api.Pokemon.GetById,         PkmRetMapper.MapTo)             as TRet,
                nameof(SpeciesRetModel)         => GetById(pId, _api.Species.GetById,         SpeciesRetMapper.MapTo)         as TRet,
                nameof(TypeRetModel)            => GetById(pId, _api.Type.GetById,            TypeRetMapper.MapTo)            as TRet,
                nameof(VersionGroupRetModel)    => GetById(pId, _api.VersionGroup.GetById,    VersionGroupRetMapper.MapTo)    as TRet,
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
