using System.Globalization;

namespace Data.Utils;
internal static class DataUrlUtils
{
    public static int? GetIdFromUrl(string pUrl)
    {
        //  Ex: https://pokeapi.co/api/v2/pokemon/1/
        string[] splits = pUrl.Split("/", StringSplitOptions.RemoveEmptyEntries);
        return int.TryParse(splits[^1], out int id) ? id : null;
    }

    public static string GetUrlFromId(int pId, string pRes)
    {
        //  Ex: https://pokeapi.co/api/v2/pokemon/1/
        return $"https://pokeapi.co/api/{Config.CurrentApiVers}/{pRes}/{pId}/";
    }

    public static string FormatNameKey(string pNameKey)
    {
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

        string nameKeyUnDashed = pNameKey.Replace("-", " ");
        return textInfo.ToTitleCase(nameKeyUnDashed);
    }

    public static void AddIdFromUrlIfExists(ICollection<int> pIds, string pResUrl)
    {
        int? id = GetIdFromUrl(pResUrl);
        if (id is null)
        {
            return;
        }

        pIds.Add(id.Value);
    }

    public static void AddIfNotExists<TData>(ISet<TData> pSet, TData pData)
    {
        if (pSet.Contains(pData))
        {
            return;
        }

        pSet.Add(pData);
    }
}
