using PkmApi.Dtos.Pokemon.Form;

using PkmDataRetrieval.Retrieval.Models.Form;

namespace PkmDataRetrieval.Adapter.Mappers
{
    internal static class FormRetMapper
    {
        public static FormRetModel MapTo(FormDto pDto)
        {
            return new()
            {
                Id = pDto.Id,
                NameKey = pDto.Name,
                SpriteFrontDefaultUrl = pDto.Sprites is not null ? pDto.Sprites.FrontDefault ?? string.Empty : string.Empty,
                TypeResUrls = GetTypes(pDto)
            };
        }

        private static IEnumerable<string> GetTypes(FormDto pDto)
        {
            static bool isValid(PkmFormTypeSdto pDto)
            {
                return pDto.Type is not null;
            }

            static string mapTo(PkmFormTypeSdto pDto)
            {
                return RetMapperUtils.GetUrl(pDto.Type);
            }

            return RetMapperUtils.GetLi(pDto.Types, isValid, mapTo);
        }
    }
}
