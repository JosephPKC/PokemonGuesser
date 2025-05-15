using PkmApi;
using PkmApi.Dtos;
using PkmApi.Dtos.Game.Generation;
using PkmApi.Dtos.Game.Pokedex;
using PkmApi.Dtos.Game.VersionGroup;
using PkmApi.Dtos.Move.Move;
using PkmApi.Dtos.Move.MoveDamageClass;
using PkmApi.Dtos.Pokemon.Form;
using PkmApi.Dtos.Pokemon.Pokemon;
using PkmApi.Dtos.Pokemon.Species;
using PkmApi.Dtos.Pokemon.Type;
using PkmApi.Endpoints;

using Data.Models;
using Data.Models.Form;
using Data.Models.Generation;
using Data.Models.Move;
using Data.Models.MoveDamageClass;
using Data.Models.Pokedex;
using Data.Models.Pokemon;
using Data.Models.Species;
using Data.Models.Type;
using Data.Models.VersionGroup;
using PkmApi.Dtos.Utility;
using Data.Models.Basic;
using Data.PkmApi.PkmApiAdapter.Mappers;

namespace Data.PkmApi.PkmApiAdapter;
internal class PkmApiAdapter : IPkmApiGateway
{
    private readonly IPkmApi _api;

    public PkmApiAdapter()
    {
        _api = PkmApiFactory.CreatePkmApi();
    }

    public PkmApiAdapter(LogWrapper.Loggers.ILoggerFactory pLogFactory)
    {
        _api = PkmApiFactory.CreatePkmApi(pLogger: pLogFactory.CreateNewLogger(typeof(IPkmApi)));
    }

    public PkmApiAdapter(IPkmApi pPkmApi)
    {
        _api = pPkmApi;
    }

    #region IPkmApiGateway
    public BasicLiDataModel? GetAll<TData>() where TData : class, IDataModel
    {
        return typeof(TData).Name switch
        {
            nameof(FormDataModel) => GetAll<TData, FormDto>(),
            nameof(GenerationDataModel) => GetAll<TData, GenerationDto>(),
            nameof(MoveDamageClassDataModel) => GetAll<TData, MoveDamageClassDto>(),
            nameof(MoveDataModel) => GetAll<TData, MoveDto>(),
            nameof(PkmDataModel) => GetAll<TData, PkmDto>(),
            nameof(PokedexDataModel) => GetAll<TData, PokedexDto>(),
            nameof(SpeciesDataModel) => GetAll<TData, SpeciesDto>(),
            nameof(TypeDataModel) => GetAll<TData, TypeDto>(),
            nameof(VersionGroupDataModel) => GetAll<TData, VersionGroupDto>(),
            _ => null
        };
    }

    public TData? GetById<TData>(int pId) where TData : class, IDataModel
    {
        return typeof(TData).Name switch
        {
            nameof(FormDataModel) => GetById<TData, FormDto>(pId),
            nameof(GenerationDataModel) => GetById<TData, GenerationDto>(pId),
            nameof(MoveDamageClassDataModel) => GetById<TData, MoveDamageClassDto>(pId),
            nameof(MoveDataModel) => GetById<TData, MoveDto>(pId),
            nameof(PkmDataModel) => GetById<TData, PkmDto>(pId),
            nameof(PokedexDataModel) => GetById<TData, PokedexDto>(pId),
            nameof(SpeciesDataModel) => GetById<TData, SpeciesDto>(pId),
            nameof(TypeDataModel) => GetById<TData, TypeDto>(pId),
            nameof(VersionGroupDataModel) => GetById<TData, VersionGroupDto>(pId),
            _ => null
        };
    }
    #endregion

    private BasicLiDataModel? GetAll<TData, TDto>() where TData : class, IDataModel where TDto : IPkmApiDto
    {
        IEndpointHandler<TDto>? endpointHandler = GetEndpointHandler<TDto>();
        if (endpointHandler is null)
        {
            return null;
        }

        //  Get All with 1 item only to get the total count first.
        ResLiDto? resInit = endpointHandler.GetAll(1, 0);
        if (resInit is null)
        {
            return null;
        }

        if (resInit.Count is null || resInit.Count == 0)
        {
            return null;
        }

        ResLiDto? res = endpointHandler.GetAll(resInit.Count.Value, 0);
        if (res is null)
        {
            return null;
        }

        IDataMapper<BasicLiDataModel, ResLiDto>? dataMapper = DataMapperFactory.CreateDataMapper<BasicLiDataModel, ResLiDto>();
        if (dataMapper is null)
        {
            return null;
        }

        return dataMapper.MapTo(res);
    }

    private TData? GetById<TData, TDto>(int pId) where TData : class, IDataModel where TDto : IPkmApiDto
    {
        IEndpointHandler<TDto>? endpointHandler = GetEndpointHandler<TDto>();
        if (endpointHandler is null)
        {
            return null;
        }

        TDto? res = endpointHandler.GetById(pId.ToString());
        if (res is null)
        {
            return null;
        }

        IDataMapper<TData, TDto>? dataMapper = DataMapperFactory.CreateDataMapper<TData, TDto>();
        if (dataMapper is null)
        {
            return null;
        }

        return dataMapper.MapTo(res);
    }

    private IEndpointHandler<TDto>? GetEndpointHandler<TDto>() where TDto : IPkmApiDto
    {
        return typeof(TDto).Name switch
        {
            nameof(FormDto) => _api.Form as IEndpointHandler<TDto>,
            nameof(GenerationDto) => _api.Generation as IEndpointHandler<TDto>,
            nameof(MoveDamageClassDto) => _api.MoveDamageClass as IEndpointHandler<TDto>,
            nameof(MoveDto) => _api.Move as IEndpointHandler<TDto>,
            nameof(PkmDto) => _api.Pokemon as IEndpointHandler<TDto>,
            nameof(PokedexDto) => _api.Pokedex as IEndpointHandler<TDto>,
            nameof(SpeciesDto) => _api.Species as IEndpointHandler<TDto>,
            nameof(TypeDto) => _api.Type as IEndpointHandler<TDto>,
            nameof(VersionGroupDto) => _api.VersionGroup as IEndpointHandler<TDto>,
            _ => null
        };
    }
}
