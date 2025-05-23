using PkmApi.Dtos.Utility;

namespace Data.PkmApi.PkmApiAdapter.Mappers;
internal abstract class BaseDataMapper
{
    protected static string GetUrl(NamedApiResDto? pRes)
    {
        return pRes?.URL ?? string.Empty;
    }

    protected static IDictionary<string, string> GetNames(IEnumerable<NameDto>? pNames)
    {
        if (pNames is null)
        {
            return new Dictionary<string, string>();
        }

        Dictionary<string, string> nameDict = [];
        foreach (NameDto name in pNames)
        {
            if (name.Name is null)
            {
                continue;
            }

            if (name.Language is null)
            {
                continue;
            }

            if (nameDict.ContainsKey(name.Language.URL))
            {
                continue;
            }

            nameDict.Add(name.Language.URL, name.Name);
        }

        return nameDict;
    }

    protected static IEnumerable<TData> GetLi<TData, TDto>(IEnumerable<TDto>? pDtos, Func<TDto, bool> pIsValid, Func<TDto, TData> pMapTo) where TData : class where TDto : class
    {
        if (pDtos is null)
        {
            return [];
        }

        List<TData> dataLi = [];
        foreach (TDto dto in pDtos)
        {
            if (!pIsValid(dto))
            {
                continue;
            }

            dataLi.Add(pMapTo(dto));
        }

        return dataLi;
    }

    protected static IEnumerable<string> GetLi(IEnumerable<NamedApiResDto>? pDtos)
    {
        static bool isValid(NamedApiResDto pDto)
        {
            return pDto is not null;
        }

        static string mapTo(NamedApiResDto pDto)
        {
            return pDto.URL;
        }

        return GetLi(pDtos, isValid, mapTo);
    }
}
