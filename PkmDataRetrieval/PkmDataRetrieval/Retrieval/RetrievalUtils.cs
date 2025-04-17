namespace PkmDataRetrieval.Retrieval
{
    internal static class RetrievalUtils
    {
        public static int? GetIdFromUrl(string pUrl)
        {
            //  Ex: https://pokeapi.co/api/v2/pokemon/1/
            string[] splits = pUrl.Split("/", StringSplitOptions.RemoveEmptyEntries);
            return int.TryParse(splits.Last(), out int id) ? id : null;
        }

        public static string GetUrlFromId(int pId, string pRes)
        {
            //  Ex: https://pokeapi.co/api/v2/pokemon/1/
            return $"https://pokeapi.co/api/{Config.CurrentApiVers}/{pRes}/{pId}/";
        }
    }
}
