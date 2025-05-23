using PkmApi.Dtos.Move.Move;

using Data.Models.Move;
using Data.PkmApi.PkmApiAdapter.Mappers;

namespace Data.PkmApi.PkmApiAdapter.Mappers.DataMappers;
internal class MoveDataMapper : BaseDataMapper, IDataMapper<MoveDataModel, MoveDto>
{
    #region IDataMapper<MoveDataModel, MoveDto>
    public MoveDataModel MapTo(MoveDto pDto)
    {
        return new()
        {
            Id = pDto.Id,
            NameKey = pDto.Name,
            Accuracy = pDto.Accuracy ?? 0,
            Power = pDto.Power ?? 0,
            Pp = pDto.Pp ?? 0,
            DamageClassResUrl = GetUrl(pDto.DamageClass),
            TypeResUrl = GetUrl(pDto.Type),
            Names = GetNames(pDto.Names),
            FlavorTextEntries = GetFlavorTextEntries(pDto)
        };
    }
    #endregion

    private static IEnumerable<FlavorTextEntryDataModel> GetFlavorTextEntries(MoveDto pDto)
    {
        static bool isValid(MoveFlavorTextSdto pDto)
        {
            return pDto.FlavorText is not null;
        }

        static FlavorTextEntryDataModel mapTo(MoveFlavorTextSdto pDto)
        {
            return new()
            {
                FlavorTextEntry = pDto.FlavorText ?? string.Empty,
                LanguageResUrl = GetUrl(pDto.Language),
                VersionGroupResUrl = GetUrl(pDto.VersionGroup)
            };
        }

        return GetLi(pDto.FlavorTextEntries, isValid, mapTo);
    }
}
