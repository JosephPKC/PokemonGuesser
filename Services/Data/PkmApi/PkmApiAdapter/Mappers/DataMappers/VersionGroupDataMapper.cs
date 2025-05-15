using PkmApi.Dtos.Game.VersionGroup;

using Data.Models.VersionGroup;
using Data.PkmApi.PkmApiAdapter.Mappers;

namespace Data.PkmApi.PkmApiAdapter.Mappers.DataMappers;
internal class VersionGroupDataMapper : BaseDataMapper, IDataMapper<VersionGroupDataModel, VersionGroupDto>
{
    #region IDataMapper<VersionGroupDataModel, VersionGroupDto>
    public VersionGroupDataModel MapTo(VersionGroupDto pDto)
    {
        return new()
        {
            Id = pDto.Id,
            NameKey = pDto.Name,
            PokedexResUrls = GetLi(pDto.Pokedexes)
        };
    }
    #endregion
}
