using PkmApi.Dtos.Pokemon.Type;

using Data.Models.Type;
using Data.PkmApi.PkmApiAdapter.Mappers;

namespace Data.PkmApi.PkmApiAdapter.Mappers.DataMappers;
internal class TypeDataMapper : BaseDataMapper, IDataMapper<TypeDataModel, TypeDto>
{
    #region IDataMapper<TypeDataModel, TypeDto>
    public TypeDataModel MapTo(TypeDto pDto)
    {
        return new()
        {
            Id = pDto.Id,
            NameKey = pDto.Name,
            Names = GetNames(pDto.Names)
        };
    }
    #endregion
}
