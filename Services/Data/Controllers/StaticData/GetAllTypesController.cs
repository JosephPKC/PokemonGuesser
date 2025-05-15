using Data.Models.Type;
using Data.PkmApi;
using Data.Utils;

namespace Data.Controllers.StaticData;
internal class GetAllTypesController(IPkmApiGateway pApi, ICacheHandler pCacheHandler, ILog pLog, int pCurrGenId)
: BaseStaticDataController(pApi, pCacheHandler, pLog, pCurrGenId)
{
    public IDictionary<string, TypeDataModel>? GetAllTypes()
    {
        return GetAllDataModelAsDict<TypeDataModel>();
    }
}
