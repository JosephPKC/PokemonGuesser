using PkmApi.Dtos.Utility;

using Data.Models.Basic;
using Data.PkmApi.PkmApiAdapter.Mappers;

namespace Data.PkmApi.PkmApiAdapter.Mappers.DataMappers;
internal class BasicDataMapper : BaseDataMapper, IDataMapper<BasicLiDataModel, ResLiDto>
{
    #region IDataMapper<BasicLiDataModel, ResLiDto>
    public BasicLiDataModel MapTo(ResLiDto pDto)
    {
        return new()
        {
            Li = GetLi(pDto)
        };
    }
    #endregion

    private static IEnumerable<BasicDataModel> GetLi(ResLiDto pDto)
    {
        if (pDto.Results is null)
        {
            return [];
        }

        ICollection<BasicDataModel> resLi = [];
        foreach (NamedApiResDto res in pDto.Results)
        {
            resLi.Add(new()
            {
                ResUrl = GetUrl(res)
            });
        }

        return resLi;
    }
}
