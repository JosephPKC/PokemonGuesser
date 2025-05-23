using Data.Models.MoveDamageClass;
using Data.PkmApi;
using Data.Utils;

namespace Data.Controllers.StaticData;
internal class GetAllMoveDamageClassesController(IPkmApiGateway pApi, ICacheHandler pCacheHandler, ILog pLog, int pCurrGenId)
: BaseStaticDataController(pApi, pCacheHandler, pLog, pCurrGenId)
{
    public IDictionary<string, MoveDamageClassDataModel>? GetAllMoveDamageClasses()
    {
        return GetAllDataModelAsDict<MoveDamageClassDataModel>();
    }
}
