using PkmApi.Dtos.Game.Generation;

using Data.Models.Generation;
using Data.PkmApi.PkmApiAdapter.Mappers;

namespace Data.PkmApi.PkmApiAdapter.Mappers.DataMappers;
internal class GenerationDataMapper : BaseDataMapper, IDataMapper<GenerationDataModel, GenerationDto>
{
    #region IDataMapper<GenerationDataModel, GenerationDto>
    public GenerationDataModel MapTo(GenerationDto pDto)
    {
        return new()
        {
            Id = pDto.Id,
            NameKey = pDto.Name,
            Names = GetNames(pDto.Names),
            VersionGroupResUrls = GetLi(pDto.VersionGroups)
        };
    }
    #endregion
}
