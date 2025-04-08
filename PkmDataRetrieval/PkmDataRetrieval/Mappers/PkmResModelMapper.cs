using PkmApi.DTOs.Shared;

using PkmDataRetrieval.Models;

namespace PkmDataRetrieval.Mappers
{
    internal static class PkmResModelMapper
    {
        public static IEnumerable<PkmResModel> MapTo(ResLiDTO pFrom)
        {
            ICollection<PkmResModel> modelLi = [];

            foreach (NamedApiResSDTO res in pFrom.Results)
            {
                int? id = ParseIdFromUrl(res.URL);
                if (!id.HasValue)
                {
                    //  Warn?
                    continue;
                }
                modelLi.Add(new()
                {
                    Name = res.Name,
                    Id = id.Value
                });
            }

            return modelLi;
        }

        private static int? ParseIdFromUrl(string pUrl)
        {
            //  Ex: https://pokeapi.co/api/v2/pokemon/1/
            string[] splits = pUrl.Split("/");
            string lastPart = splits.Last();
            return int.TryParse(lastPart, out int id) ? id : null;
        }
    }
}
