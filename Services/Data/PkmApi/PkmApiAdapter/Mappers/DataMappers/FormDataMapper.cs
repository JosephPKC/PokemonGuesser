using PkmApi.Dtos.Pokemon.Form;

using Data.Models.Form;
using Data.PkmApi.PkmApiAdapter.Mappers;

namespace Data.PkmApi.PkmApiAdapter.Mappers.DataMappers;
internal class FormDataMapper : BaseDataMapper, IDataMapper<FormDataModel, FormDto>
{
    #region IDataMapper<FormDataModel, FormDto>
    public FormDataModel MapTo(FormDto pDto)
    {
        return new()
        {
            Id = pDto.Id,
            NameKey = pDto.Name,
            SpriteFrontDefaultUrl = pDto.Sprites is not null ? pDto.Sprites.FrontDefault ?? string.Empty : string.Empty,
            TypeResUrls = GetTypes(pDto)
        };
    }
    #endregion

    private static IEnumerable<string> GetTypes(FormDto pDto)
    {
        static bool isValid(PkmFormTypeSdto pDto)
        {
            return pDto.Type is not null;
        }

        static string mapTo(PkmFormTypeSdto pDto)
        {
            return GetUrl(pDto.Type);
        }

        return GetLi(pDto.Types, isValid, mapTo);
    }
}
