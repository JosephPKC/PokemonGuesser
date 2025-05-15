using Data.Models.Generation;
using Data.PkmApi;
using Data.Utils;

namespace Data.Controllers;
internal class GetAllVersGrpIdsController(IPkmApiGateway pApi, ICacheHandler pCacheHandler, ILog pLog, int pCurrGenId)
    : BaseController(pApi, pCacheHandler, pLog, pCurrGenId)
{
    public ISet<int> GetAllVersGrpIds()
    {
        HashSet<int> versGrpIds = [];

        GenerationDataModel? genRet = GetDataModelById<GenerationDataModel>(_currGenId);
        if (genRet is null)
        {
            return versGrpIds;
        }

        foreach (string versGrpUrl in genRet.VersionGroupResUrls)
        {
            DataUrlUtils.AddIdFromUrlIfExists(versGrpIds, versGrpUrl);
        }

        return versGrpIds;
    }


}
