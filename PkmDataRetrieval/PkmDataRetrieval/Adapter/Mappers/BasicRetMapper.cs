using PkmApi.Dtos.Utility;

using PkmDataRetrieval.Retrieval.Models;

namespace PkmDataRetrieval.Adapter.Mappers
{
    internal static class BasicRetMapper
    {
        public static BasicRetModel MapTo(NamedApiResDto pDto)
        {
            return new()
            {
                ResUrl = RetMapperUtils.GetUrl(pDto)
            };
        }

        public static IEnumerable<BasicRetModel> MapTo(ResLiDto pDto)
        {
            if (pDto.Results is null)
            {
                return [];
            }

            ICollection<BasicRetModel> resLi = [];
            foreach (NamedApiResDto res in pDto.Results)
            {
                resLi.Add(MapTo(res));
            }

            return resLi;
        }
    }
}
