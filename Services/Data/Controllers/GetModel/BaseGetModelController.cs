using Data.Models;
using Data.PkmApi;
using Data.Utils;

namespace Data.Controllers.GetModel;
internal abstract class BaseGetModelController(IPkmApiGateway pApi, ICacheHandler pCacheHandler, ILog pLog, int pCurrGenId, ISet<int> pCurrVersGrpIds, string pKeyPrefix, StaticDataLookUp pStaticData)
    : BaseController(pApi, pCacheHandler, pLog, pCurrGenId)
{
    protected readonly StaticDataLookUp _staticData = pStaticData;
    protected readonly string _keyPrefix = pKeyPrefix;
    protected readonly ISet<int> _currVersGrpIds = pCurrVersGrpIds;

    protected static bool IsAltForm(string pName)
    {
        //  TODO: Need a better way of determining alt forms.
        //  For now, hardcoded to include all paldean forms only.
        return pName.Contains("PALDEA", StringComparison.CurrentCultureIgnoreCase);
    }
}
