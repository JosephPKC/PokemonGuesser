using PkmApi.Dtos;

using Data.Models;

namespace Data.PkmApi.PkmApiAdapter.Mappers;
public interface IDataMapper<TData, TDto> : IDataMapper where TData : IDataModel where TDto : IPkmApiDto
{
    TData MapTo(TDto pDto);
}

public interface IDataMapper { }
