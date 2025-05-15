using PkmApi.Dtos.Move.MoveDamageClass;

using Data.Models.MoveDamageClass;
using Data.PkmApi.PkmApiAdapter.Mappers;

namespace Data.PkmApi.PkmApiAdapter.Mappers.DataMappers;
internal class MoveDamageClassDataMapper : BaseDataMapper, IDataMapper<MoveDamageClassDataModel, MoveDamageClassDto>
{
    #region IDataMapper<MoveDamageClassDataModel, MoveDamageClassDto>
    public MoveDamageClassDataModel MapTo(MoveDamageClassDto pDto)
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
