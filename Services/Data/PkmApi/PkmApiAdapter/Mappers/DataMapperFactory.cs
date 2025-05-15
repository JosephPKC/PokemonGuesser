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
using PkmApi.Dtos.Utility;

using Data.Models;
using Data.Models.Basic;
using Data.Models.Form;
using Data.Models.Generation;
using Data.Models.Move;
using Data.Models.MoveDamageClass;
using Data.Models.Pokedex;
using Data.Models.Pokemon;
using Data.Models.Species;
using Data.Models.Type;
using Data.Models.VersionGroup;
using Data.PkmApi.Mappers.DataMappers;

namespace Data.PkmApi.PkmApiAdapter.Mappers;
public static class DataMapperFactory
{
    public static IDataMapper<TData, TDto>? CreateDataMapper<TData, TDto>() where TData : IDataModel where TDto : IPkmApiDto
    {
        return (typeof(TData).Name, typeof(TDto).Name) switch
        {
            (nameof(BasicLiDataModel), nameof(ResLiDto)) => new BasicDataMapper() as IDataMapper<TData, TDto>,
            (nameof(FormDataModel), nameof(FormDto)) => new FormDataMapper() as IDataMapper<TData, TDto>,
            (nameof(GenerationDataModel), nameof(GenerationDto)) => new GenerationDataMapper() as IDataMapper<TData, TDto>,
            (nameof(MoveDamageClassDataModel), nameof(MoveDamageClassDto)) => new MoveDamageClassDataMapper() as IDataMapper<TData, TDto>,
            (nameof(MoveDataModel), nameof(MoveDto)) => new MoveDataMapper() as IDataMapper<TData, TDto>,
            (nameof(PkmDataModel), nameof(PkmDto)) => new PkmDataMapper() as IDataMapper<TData, TDto>,
            (nameof(PokedexDataModel), nameof(PokedexDto)) => new PokedexDataMapper() as IDataMapper<TData, TDto>,
            (nameof(SpeciesDataModel), nameof(SpeciesDto)) => new SpeciesDataMapper() as IDataMapper<TData, TDto>,
            (nameof(TypeDataModel), nameof(TypeDto)) => new TypeDataMapper() as IDataMapper<TData, TDto>,
            (nameof(VersionGroupDataModel), nameof(VersionGroupDto)) => new VersionGroupDataMapper() as IDataMapper<TData, TDto>,
            _ => null
        };
    }
}
